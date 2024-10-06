using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Auth.Commands;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtService(
        JwtConfiguration configuration,
        IIdentityService identityService,
        IHttpContextAccessor httpContextAccessor,
        IDbContext dbContext) : IJwtService
{
    public async Task<SigninCommand.JwtTokens> GenerateTokenAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
    {
        var email = Email.From(claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Email).Value);
        var user = await identityService.GetUserAsync(email);
        if (user is null)
        {
            throw CustomException.WithBadRequest("No user details found");
        }

        var refreshToken = await GenerateRefreshTokenAsync(user, cancellationToken);
        var accessToken = await GenerateAccessToken(claimsPrincipal, cancellationToken);
        return new SigninCommand.JwtTokens(accessToken, refreshToken);
    }

    public Email ExtractEmailFromToken(string accessToken)
    {
        var principal = GetPrincipalFromAccessToken(accessToken);
        var emailString = principal.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        return Email.From(emailString);
    }

    public async Task<bool> ValidateRefreshTokenAsync(User user, string value, CancellationToken cancellationToken)
    {
        var deviceIdentifier = httpContextAccessor.GetDeviceIdentifier();

        var refreshToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == user.Id && x.DeviceIdentifier == deviceIdentifier, cancellationToken);

        if (refreshToken != null)
        {
            return refreshToken.ModifiedOn.AddMinutes(configuration.RefreshTokenLifetimeInMinutes) > DateTime.UtcNow;
        }

        return false;
    }

    private ClaimsPrincipal GetPrincipalFromAccessToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(configuration.SecretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid access token");
        }

        return principal;
    }

    private async Task<string> GenerateAccessToken(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
    {
        var claims = claimsPrincipal.Claims.ToList();
        await AddCustomClaimsAsync(claims, cancellationToken);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.AccessTokenLifetimeInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration.Audience,
            Issuer = configuration.Issuer,
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private async Task<string> GenerateRefreshTokenAsync(User user, CancellationToken cancellationToken)
    {
        var deviceIdentifier = httpContextAccessor.GetDeviceIdentifier();
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var value = Convert.ToBase64String(randomNumber);

        var refreshToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == user.Id && x.DeviceIdentifier == deviceIdentifier, cancellationToken);

        if (refreshToken is null)
        {
            await dbContext.RefreshTokens.AddAsync(RefreshToken.Create(user.Id, value, deviceIdentifier), cancellationToken);
        }
        else
        {
            refreshToken.Update(value);
        }

        if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
        {
            return value;
        }

        throw CustomException.WithInternalServer("Unable to save refresh token");
    }

    private async Task AddCustomClaimsAsync(List<Claim> claims, CancellationToken cancellationToken)
    {
        var userId = int.Parse(claims.Find(x => x.Type == ClaimTypes.NameIdentifier)!.Value);

        var employee = await dbContext
            .Employees
            .IgnoreQueryFilters()
            .Where(x => x.UserId == userId)
            .Select(x => new
            {
                x.Id,
                x.FullName,
                x.CompanyId,
                x.DateOfJoining
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (employee != null)
        {
            claims.Add(new Claim("employeeId", employee.Id.ToString()!));
            claims.Add(new Claim("name", employee.FullName.ToString()!));
            claims.Add(new Claim("companyId", employee.CompanyId.ToString()));
            claims.Add(new Claim("dateOfJoining", employee.DateOfJoining.ToString()));
        }
        else
        {
            var companyId = (await dbContext.Companies
                .Where(x => x.OwnerUserId == userId)
                .FirstOrDefaultAsync(cancellationToken))
                 ?.Id;

            if (companyId != null)
            {
                claims.Add(new Claim("companyId", companyId.Value.ToString()));
            }
        }
    }
}

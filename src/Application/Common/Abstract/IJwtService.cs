using System.Security.Claims;
using Application.Auth.Commands;
using Domain.Identity;

namespace Application.Common.Abstract;

public interface IJwtService
{
    Task<SigninCommand.JwtTokens> GenerateTokenAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken);

    Email ExtractEmailFromToken(string accessToken);

    Task<bool> ValidateRefreshTokenAsync(User user, string value, CancellationToken cancellationToken);
}

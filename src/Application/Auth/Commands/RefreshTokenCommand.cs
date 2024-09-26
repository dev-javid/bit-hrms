using Microsoft.AspNetCore.Http;

namespace Application.Auth.Commands;

public class RefreshTokenCommand : IRequest<SigninCommand.JwtTokens>
{
    public string RefreshToken { get; set; } = null!;

    public string AccessToken { get; set; } = null!;

    public class Validator : AbstractValidator<RefreshTokenCommand>
    {
        public Validator(IIdentityService identityService, IJwtService jwtService)
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty();

            RuleFor(x => x.AccessToken)
                .NotEmpty();

            RuleFor(x => x)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var email = jwtService.ExtractEmailFromToken(x.AccessToken);
                    var user = await identityService.GetUserAsync(email);
                    if (user != null)
                    {
                        return await jwtService.ValidateRefreshTokenAsync(user, x.RefreshToken, cancellationToken);
                    }

                    return false;
                })
                .WithMessage("Token expired or invalid.");
        }
    }

    internal class Handler(IJwtService jwtService, IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RefreshTokenCommand, SigninCommand.JwtTokens>
    {
        public async Task<SigninCommand.JwtTokens> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var email = jwtService.ExtractEmailFromToken(request.AccessToken);
            var user = await identityService.GetUserAsync(email);
            await identityService.SigninAsync(user!);
            return await jwtService.GenerateTokenAsync(httpContextAccessor.HttpContext!.User, cancellationToken);
        }
    }
}

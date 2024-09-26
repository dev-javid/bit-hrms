using Microsoft.AspNetCore.Http;

namespace Application.Auth.Commands;

public class SigninCommand : IRequest<SigninCommand.JwtTokens>
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public class Validator : AbstractValidator<SigninCommand>
    {
        public Validator(IIdentityService identityService)
        {
            RuleFor(x => x)
                .MustAsync(async (x, cancellationToken) =>
                    await identityService.VerifyCredentialsAsync(x.Email.ToValueObject<Email>(), x.Password))
                .WithMessage("Incorrect email or password.");
        }
    }

    public record JwtTokens(string AccessToken, string RefreshToken);

    internal class Handler(IJwtService jwtService, IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<SigninCommand, JwtTokens>
    {
        public async Task<JwtTokens> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            var email = Domain.Common.ValueObjects.Email.From(request.Email);
            var user = await identityService.GetUserAsync(email);
            await identityService.SigninAsync(user!);
            return await jwtService.GenerateTokenAsync(httpContextAccessor.HttpContext!.User, cancellationToken);
        }
    }
}

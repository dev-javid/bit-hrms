using Application.Common.MediatR;

namespace Application.Auth.Commands;

public class ResetPasswordCommand : IUpdateCommand
{
    public int UserId { get; init; }

    public string Token { get; init; } = null!;

    public string Password { get; init; } = null!;

    public class Validator : AbstractValidator<ResetPasswordCommand>
    {
        public Validator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.Token)
                .NotEmpty();

            RuleFor(x => x.Password)
               .NotEmpty();
        }
    }

    internal class Handler(IIdentityService identityService) : IUpdateCommandHandler<ResetPasswordCommand>
    {
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await identityService.ResetPasswordAsync(request.UserId, request.Token, request.Password);
            return true;
        }
    }
}

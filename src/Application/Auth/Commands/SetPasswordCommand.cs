using Application.Common.MediatR;

namespace Application.Auth.Commands;

public class SetPasswordCommand : IMediatRCommand
{
    public int UserId { get; init; }

    public string Token { get; init; } = null!;

    public string Password { get; init; } = null!;

    internal class Handler(IIdentityService identityService) : IMediatRCommandHandler<SetPasswordCommand>
    {
        public async Task<MediatRResponse> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            await identityService.VerifyAccountAsync(request.UserId, request.Token, request.Password);
            return MediatRResponse.Success();
        }
    }
}

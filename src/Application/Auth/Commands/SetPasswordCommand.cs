namespace Application.Auth.Commands;

public class SetPasswordCommand : IUpdateCommand
{
    public int UserId { get; init; }

    public string Token { get; init; } = null!;

    public string Password { get; init; } = null!;

    internal class Handler(IIdentityService identityService) : IUpdateCommandHandler<SetPasswordCommand>
    {
        public async Task<bool> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            await identityService.VerifyAccountAsync(request.UserId, request.Token, request.Password);
            return true;
        }
    }
}

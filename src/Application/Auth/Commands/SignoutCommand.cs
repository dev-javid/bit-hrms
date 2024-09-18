namespace Application.Auth.Commands;

public class SignoutCommand : IRequest
{
    internal class Handler(ICurrentUser currentUser, IIdentityService identityService) : IRequestHandler<SignoutCommand>
    {
        public async Task Handle(SignoutCommand request, CancellationToken cancellationToken)
        {
            await identityService.LogoutAsync(currentUser.Id);
        }
    }
}

using Application.Common.MediatR;

namespace Application.Auth.Commands;

public class UpdatePasswordCommand : IUpdateCommand
{
    public string CurrentPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;

    internal class Handler(IIdentityService identityService, ICurrentUser currentUser) : IUpdateCommandHandler<UpdatePasswordCommand>
    {
        public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            await identityService.ChangePasswordAsync(currentUser.Id, request.CurrentPassword, request.NewPassword);
            return true;
        }
    }
}

using Application.Auth.Commands;

namespace Presentation.Controllers
{
    public class AuthController : ApiBaseController
    {
        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SigninAsync(SigninCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("sign-out")]
        [AllowAnonymous]
        public async Task<IActionResult> SignoutAsync(CancellationToken cancellationToken)
        {
            await Mediator.Send(new SignoutCommand(), cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("set-password")]
        public async Task<IActionResult> SetPasswordAsyn(SetPasswordCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsyn(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("update-password")]
        [Authorize(AuthPolicy.AllRoles)]
        public async Task<IActionResult> UpdatePasswordAsyn(UpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsyn(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}

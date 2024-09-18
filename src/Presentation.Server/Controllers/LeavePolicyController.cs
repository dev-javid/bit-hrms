using Application.Companies.Queries;
using Application.LeavePolicies.Commands.SetLeavePolicy;

namespace Presentation.Controllers
{
    [Route("api/leave-policy")]
    public class LeavePolicyController : ApiBaseController
    {
        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await Mediator.Send(new GetLeavePolicyQuery()));
        }

        [HttpPut]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> SetAsync(SetLeavePolicyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

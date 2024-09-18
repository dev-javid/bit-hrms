using Application.Compensatios.Commands;
using Application.Compensatios.Query;

namespace Presentation.Controllers
{
    public class CompensationsController : ApiBaseController
    {
        [HttpPost]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> AddAsync(AddCompensationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetAsync([FromQuery] GetCompensationsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}

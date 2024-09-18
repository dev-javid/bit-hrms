using Application.Salaries.Commands;
using Application.Salaries.Queries;

namespace Presentation.Controllers
{
    [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
    public class SalariesController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetSalariesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("estimated-salary")]
        public async Task<IActionResult> GetEstimatedSalaryAsync([FromQuery] GetEstimatedSaleryQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> AddSalaryAsync(AddSalaryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

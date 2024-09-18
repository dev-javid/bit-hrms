using Application.Regularization.Commands;
using Application.Regularization.Queries;

namespace Presentation.Controllers
{
    [Route("api/attendance-regularizations")]
    public class AttendanceRegularizationsController : ApiBaseController
    {
        [HttpPut("{attendanceRegularizationId}/approve")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> ApproveAsync(int attendanceRegularizationId, ApproveRegularizationCommand command)
        {
            command.AttendanceRegularizationId = attendanceRegularizationId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Authorize(AuthPolicy.Employee)]
        public async Task<IActionResult> RequestAsync(AddAttendanceRegularizationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetRegularizationAsync([FromQuery] GetRegularizationsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}

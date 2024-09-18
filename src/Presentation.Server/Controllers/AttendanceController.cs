using Application.Attendance.Commands;
using Application.Attendance.Queries;

namespace Presentation.Controllers
{
    public class AttendanceController : ApiBaseController
    {
        [HttpPut("clock-out")]
        [Authorize(AuthPolicy.Employee)]
        public async Task<IActionResult> ClockOutAsync(ClockOutCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("clock-In")]
        [Authorize(AuthPolicy.Employee)]
        public async Task<IActionResult> ClockInAsync(ClockInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetAttendanceAsync([FromQuery] GetAttendanceQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}

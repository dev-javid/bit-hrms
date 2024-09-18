using Application.EmployeeLeaves.Command;
using Application.EmployeeLeaves.Queries;

namespace Presentation.Controllers
{
    [Route("api/employee-leaves")]
    public class EmployeeLeavesController : ApiBaseController
    {
        [HttpPost]
        [Authorize(AuthPolicy.Employee)]
        public async Task<IActionResult> ApplyAsync(ApplyLeaveCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetEmployeeLeavesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpDelete("{employeeLeaveId}")]
        [Authorize(AuthPolicy.Employee)]
        public async Task<IActionResult> DeleteAsync(int employeeLeaveId)
        {
            await Mediator.Send(new DeleteLeaveCommand
            {
                EmployeeLeaveId = employeeLeaveId
            });
            return Ok();
        }

        [HttpPut("{employeeLeaveId}/approve")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> ApproveAsync(int employeeLeaveId, ApproveLeaveCommand command)
        {
            command.EmployeeLeaveId = employeeLeaveId;
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("{employeeLeaveId}/decline")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> DeclineAsync(int employeeLeaveId, DeclineLeaveCommand command)
        {
            command.EmployeeLeaveId = employeeLeaveId;
            await Mediator.Send(command);
            return Ok();
        }
    }
}

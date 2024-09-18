using Application.Employees.Commands;
using Application.Employees.Queries;

namespace Presentation.Controllers
{
    public class EmployeesController : ApiBaseController
    {
        [HttpPost]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> AddAsync(AddEmployeeCommand command)
        {
            var employeeId = await Mediator.Send(command);
            return Ok(await GetByIdAsync(employeeId));
        }

        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetEmployeesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{employeeId}")]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetByIdAsync(int employeeId)
        {
            return Ok(await Mediator.Send(new GetEmployeeQuery
            {
                EmployeeId = employeeId
            }));
        }

        [HttpPut("{employeeId}/documents")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> UpdateAsync(int employeeId, SetEmployeeDocumentCommand command)
        {
            command.EmployeeId = employeeId;
            await Mediator.Send(command);
            return await GetByIdAsync(employeeId);
        }
    }
}

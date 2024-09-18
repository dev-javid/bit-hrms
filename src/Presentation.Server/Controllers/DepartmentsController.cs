using Application.Departments.Commands;
using Application.Departments.Queries;

namespace Presentation.Controllers
{
    [Authorize(AuthPolicy.CompanyAdmin)]
    public class DepartmentsController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetDepartmentsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddDepartmentCommand command)
        {
            var id = await Mediator.Send(command);
            return await GetByIdAsync(id);
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> UpdateAsync(int departmentId, UpdateDepartmentCommand command)
        {
            command.DepartmentId = departmentId;
            await Mediator.Send(command);
            return await GetByIdAsync(departmentId);
        }

        [HttpGet("{departmentId}")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> GetByIdAsync(int departmentId)
        {
            return Ok(await Mediator.Send(new GetDepartmentQuery
            {
                DepartmentId = departmentId
            }));
        }
    }
}

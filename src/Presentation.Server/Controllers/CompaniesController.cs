using Application.Companies.Commands;
using Application.Companies.Queries;

namespace Presentation.Controllers
{
    [Authorize(AuthPolicy.SuperAdmin)]
    public class CompaniesController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCompaniesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCompanyCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(new
            {
                Id = id
            });
        }
    }
}

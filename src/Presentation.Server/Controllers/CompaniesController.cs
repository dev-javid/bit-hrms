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

        [HttpGet("{companyId}")]
        [Authorize(AuthPolicy.Employee)]
        public async Task<IActionResult> GetAsync(int companyId)
        {
            return Ok(await Mediator.Send(new GetCompanyQuery
            {
                CompanyId = companyId
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCompanyCommand command)
        {
            var id = await Mediator.Send(command);
            return await GetAsync(id);
        }
    }
}

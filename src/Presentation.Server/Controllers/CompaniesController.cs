using Application.Companies.Commands;
using Application.Companies.Queries;

namespace Presentation.Controllers
{
    public class CompaniesController : ApiBaseController
    {
        [HttpGet]
        [Authorize(AuthPolicy.SuperAdmin)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCompaniesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{companyId}")]
        [Authorize(AuthPolicy.AllRoles)]
        public async Task<IActionResult> GetAsync(int companyId)
        {
            var company = await Mediator.Send(new GetCompanyQuery
            {
                CompanyId = companyId
            });

            return company != null ? Ok(company) : NotFound();
        }

        [HttpPost]
        [Authorize(AuthPolicy.SuperAdmin)]
        public async Task<IActionResult> AddAsync(AddCompanyCommand command)
        {
            var response = await Mediator.Send(command);
            return await GetAsync(response.Value);
        }

        [HttpPut("{companyId}")]
        [Authorize(AuthPolicy.SuperAdmin)]
        public async Task<IActionResult> UpdateAsync(int companyId, UpdateCompanyCommand command)
        {
            command.CompanyId = companyId;
            var response = await Mediator.Send(command);
            return await GetAsync(response.Value);
        }

        [HttpDelete("{companyId}")]
        [Authorize(AuthPolicy.SuperAdmin)]
        public async Task<IActionResult> DeleteAsync(int companyId)
        {
            await Mediator.Send(new DeleteCompanyCommand
            {
                CompanyId = companyId
            });

            return Ok();
        }
    }
}

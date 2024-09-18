using Application.IncomeSources.Commands;
using Application.IncomeSources.Queries;

namespace Presentation.Controllers;

[Route("api/income-sources")]
public class IncomeSourcesController : ApiBaseController
{
    [HttpPost]
    [Authorize(AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> AddAsync(AddIncomeSourceCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(new
        {
            Id = id
        });
    }

    [HttpGet]
    [Authorize(AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetIncomeSourcesQuery query)
    {
        var incomeSources = await Mediator.Send(query);
        return Ok(incomeSources);
    }
}

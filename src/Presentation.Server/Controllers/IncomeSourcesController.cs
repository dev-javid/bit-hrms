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
        var response = await Mediator.Send(command);
        return Ok(new
        {
            Id = response.Value
        });
    }

    [HttpGet]
    [Authorize(AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetIncomeSourcesQuery query)
    {
        var incomeSources = await Mediator.Send(query);
        return Ok(incomeSources);
    }

    [HttpPut("{incomeSourceId}")]
    [Authorize(AuthPolicy.CompanyAdmin)]
    [ProducesResponseType<GetIncomeSourcesQuery.Response>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(int incomeSourceId, UpdateIncomeSourceCommand command)
    {
        command.IncomeSourceId = incomeSourceId;
        await Mediator.Send(command);
        return Ok();
    }
}

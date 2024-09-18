using Application.Incomes.Commands;
using Application.Incomes.Queries;

namespace Presentation.Controllers;

public class IncomesController : ApiBaseController
{
    [HttpGet]
    [Authorize(AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> GetAsync([FromQuery] GetIncomesQuery query)
    {
        var incomes = await Mediator.Send(query);
        return Ok(incomes);
    }

    [HttpPost]
    [Authorize(AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> AddAsync(AddIncomeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}

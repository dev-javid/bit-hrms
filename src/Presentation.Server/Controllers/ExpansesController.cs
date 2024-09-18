using Application.Expanses.Commands;
using Application.Expanses.Queries;

namespace Presentation.Controllers;

public class ExpansesController : ApiBaseController
{
    [HttpGet]
    [Authorize(Policy = AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> GetExpanses([FromQuery] GetExpansesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize(Policy = AuthPolicy.CompanyAdmin)]
    public async Task<ActionResult<int>> AddExpanse(AddExpanseCommand command)
    {
        return await Mediator.Send(command);
    }
}

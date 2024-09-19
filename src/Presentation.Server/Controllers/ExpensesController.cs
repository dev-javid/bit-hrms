using Application.Expenses.Commands;
using Application.Expenses.Queries;

namespace Presentation.Controllers;

public class ExpensesController : ApiBaseController
{
    [HttpGet]
    [Authorize(Policy = AuthPolicy.CompanyAdmin)]
    public async Task<IActionResult> GetExpenses([FromQuery] GetExpensesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize(Policy = AuthPolicy.CompanyAdmin)]
    public async Task<ActionResult<int>> AddExpense(AddExpenseCommand command)
    {
        return await Mediator.Send(command);
    }
}

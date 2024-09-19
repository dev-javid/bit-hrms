namespace Tests.Unit.Presentation.Controllers;

public class ExpensesControllerTests : ControllerTests<ExpensesController>
{
    [Theory]
    [InlineData(nameof(ExpensesController.GetExpenses), AuthPolicy.CompanyAdmin)]
    [InlineData(nameof(ExpensesController.AddExpense), AuthPolicy.CompanyAdmin)]
    public override void Authorization_Tests(string methodName, string expectedPolicy)
    {
        RunAuthorizationTest(methodName, expectedPolicy);
    }
}

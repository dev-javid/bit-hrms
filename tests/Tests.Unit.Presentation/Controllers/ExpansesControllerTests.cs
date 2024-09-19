namespace Tests.Unit.Presentation.Controllers;

public class ExpansesControllerTests : ControllerTests<ExpansesController>
{
    [Theory]
    [InlineData(nameof(ExpansesController.GetExpanses), AuthPolicy.CompanyAdmin)]
    [InlineData(nameof(ExpansesController.AddExpanse), AuthPolicy.CompanyAdmin)]
    public override void Authorization_Tests(string methodName, string expectedPolicy)
    {
        RunAuthorizationTest(methodName, expectedPolicy);
    }
}

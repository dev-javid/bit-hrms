namespace Tests.Unit.Presentation.Controllers;

public class IncomeSourcesControllerTests : ControllerTests<IncomeSourcesController>
{
    [Theory]
    [InlineData(nameof(IncomeSourcesController.AddAsync), AuthPolicy.CompanyAdmin)]
    [InlineData(nameof(IncomeSourcesController.GetAllAsync), AuthPolicy.CompanyAdmin)]
    public override void Authorization_Tests(string methodName, string expectedPolicy)
    {
        RunAuthorizationTest(methodName, expectedPolicy);
    }
}

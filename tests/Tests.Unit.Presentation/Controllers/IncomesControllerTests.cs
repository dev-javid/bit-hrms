namespace Tests.Unit.Presentation.Controllers;

public class IncomesControllerTests : ControllerTests<IncomesController>
{
    [Theory]
    [InlineData(nameof(IncomesController.GetAsync), AuthPolicy.CompanyAdmin)]
    [InlineData(nameof(IncomesController.AddAsync), AuthPolicy.CompanyAdmin)]
    public override void Authorization_Tests(string methodName, string expectedPolicy)
    {
        RunAuthorizationTest(methodName, expectedPolicy);
    }
}

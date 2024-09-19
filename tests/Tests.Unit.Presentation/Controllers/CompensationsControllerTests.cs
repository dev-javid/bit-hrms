namespace Tests.Unit.Presentation.Controllers
{
    public class CompensationsControllerTests : ControllerTests<CompensationsController>
    {
        [Theory]
        [InlineData(nameof(CompensationsController.AddAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(CompensationsController.GetAsync), AuthPolicy.CompanyAdminOrEmployee)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

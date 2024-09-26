namespace Tests.Unit.Presentation.Controllers
{
    public class CompaniesControllerTests : ControllerTests<CompaniesController>
    {
        [Theory]
        [InlineData(nameof(CompaniesController.GetAllAsync), AuthPolicy.SuperAdmin)]
        [InlineData(nameof(CompaniesController.GetAsync), AuthPolicy.Employee)]
        [InlineData(nameof(CompaniesController.AddAsync), AuthPolicy.SuperAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

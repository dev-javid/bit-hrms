namespace Tests.Unit.Presentation.Controllers
{
    public class SalariesControllerTests : ControllerTests<SalariesController>
    {
        [Theory]
        [InlineData(nameof(SalariesController.GetAllAsync), AuthPolicy.CompanyAdminOrEmployee)]
        [InlineData(nameof(SalariesController.GetEstimatedSalaryAsync), AuthPolicy.CompanyAdminOrEmployee)]
        [InlineData(nameof(SalariesController.AddSalaryAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

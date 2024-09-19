namespace Tests.Unit.Presentation.Controllers
{
    public class ReportsControllerTests : ControllerTests<ReportsController>
    {
        [Theory]
        [InlineData(nameof(ReportsController.GetAdminBasicReportAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(ReportsController.GetEmployeeBasicReportAsync), AuthPolicy.CompanyAdminOrEmployee)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

namespace Tests.Unit.Presentation.Controllers
{
    public class AttendanceRegularizationControllerTests : ControllerTests<AttendanceRegularizationsController>
    {
        [Theory]
        [InlineData(nameof(AttendanceRegularizationsController.ApproveAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(AttendanceRegularizationsController.RequestAsync), AuthPolicy.Employee)]
        [InlineData(nameof(AttendanceRegularizationsController.GetRegularizationAsync), AuthPolicy.CompanyAdminOrEmployee)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

namespace Tests.Unit.Presentation.Controllers
{
    public class AttendanceControllerTests : ControllerTests<AttendanceController>
    {
        [Theory]
        [InlineData(nameof(AttendanceController.ClockOutAsync), AuthPolicy.Employee)]
        [InlineData(nameof(AttendanceController.ClockInAsync), AuthPolicy.Employee)]
        [InlineData(nameof(AttendanceController.GetAttendanceAsync), AuthPolicy.CompanyAdminOrEmployee)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

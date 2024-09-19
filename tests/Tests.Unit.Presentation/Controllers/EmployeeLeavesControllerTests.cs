namespace Tests.Unit.Presentation.Controllers
{
    public class EmployeeLeavesControllerTests : ControllerTests<EmployeeLeavesController>
    {
        [Theory]
        [InlineData(nameof(EmployeeLeavesController.ApplyAsync), AuthPolicy.Employee)]
        [InlineData(nameof(EmployeeLeavesController.GetAllAsync), AuthPolicy.CompanyAdminOrEmployee)]
        [InlineData(nameof(EmployeeLeavesController.DeleteAsync), AuthPolicy.Employee)]
        [InlineData(nameof(EmployeeLeavesController.ApproveAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(EmployeeLeavesController.DeclineAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

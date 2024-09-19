namespace Tests.Unit.Presentation.Controllers
{
    public class EmployeesControllerTests : ControllerTests<EmployeesController>
    {
        [Theory]
        [InlineData(nameof(EmployeesController.AddAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(EmployeesController.GetByIdAsync), AuthPolicy.CompanyAdminOrEmployee)]
        [InlineData(nameof(EmployeesController.GetAllAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(EmployeesController.UpdateAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

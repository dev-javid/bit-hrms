namespace Tests.Unit.Presentation.Controllers
{
    public class DepartmentsControllerTests : ControllerTests<DepartmentsController>
    {
        [Theory]
        [InlineData(nameof(DepartmentsController.GetAllAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(DepartmentsController.AddAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(DepartmentsController.UpdateAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

namespace Tests.Unit.Presentation.Controllers
{
    public class LeavePolicyControllerTests : ControllerTests<LeavePolicyController>
    {
        [Theory]
        [InlineData(nameof(LeavePolicyController.GetAsync), AuthPolicy.CompanyAdminOrEmployee)]
        [InlineData(nameof(LeavePolicyController.SetAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

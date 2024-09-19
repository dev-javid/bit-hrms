namespace Tests.Unit.Presentation.Controllers
{
    public class UsersControllerTests : ControllerTests<UsersController>
    {
        [Theory]
        [InlineData(nameof(UsersController.AddAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(UsersController.GetAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

namespace Tests.Unit.Presentation.Controllers
{
    public class AuthControllerTests : ControllerTests<AuthController>
    {
        [Theory]
        [InlineData(nameof(AuthController.SigninAsync), AuthPolicy.AllowAnonymous)]
        [InlineData(nameof(AuthController.RefreshTokenAsync), AuthPolicy.AllowAnonymous)]
        [InlineData(nameof(AuthController.SignoutAsync), AuthPolicy.AllowAnonymous)]
        [InlineData(nameof(AuthController.SetPasswordAsyn), AuthPolicy.AllowAnonymous)]
        [InlineData(nameof(AuthController.ResetPasswordAsyn), AuthPolicy.AllowAnonymous)]
        [InlineData(nameof(AuthController.UpdatePasswordAsyn), AuthPolicy.AllRoles)]
        [InlineData(nameof(AuthController.ForgotPasswordAsyn), AuthPolicy.AllowAnonymous)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

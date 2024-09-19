namespace Tests.Unit.Presentation.Controllers
{
    public class HolidaysControllerTests : ControllerTests<HolidaysController>
    {
        [Theory]
        [InlineData(nameof(HolidaysController.GetAllAsync), AuthPolicy.CompanyAdminOrEmployee)]
        [InlineData(nameof(HolidaysController.AddAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(HolidaysController.UpdateAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

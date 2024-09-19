namespace Tests.Unit.Presentation.Controllers
{
    public class JobTitlesControllerTests : ControllerTests<JobTitlesController>
    {
        [Theory]
        [InlineData(nameof(JobTitlesController.GetAllAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(JobTitlesController.AddAsync), AuthPolicy.CompanyAdmin)]
        [InlineData(nameof(JobTitlesController.UpdateAsync), AuthPolicy.CompanyAdmin)]
        public override void Authorization_Tests(string methodName, string expectedPolicy)
        {
            RunAuthorizationTest(methodName, expectedPolicy);
        }
    }
}

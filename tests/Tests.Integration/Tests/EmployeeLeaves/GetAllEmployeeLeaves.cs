namespace Tests.Integration.Tests.EmployeeLeaves
{
    public class GetAllEmployeeLeaves(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "api/employee-leaves?getAll=True";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/EmployeeLeaves/GetAllEmployeeLeaves.sql");
            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

namespace Tests.Integration.Tests.Reports
{
    public class GetAdminBasicReports(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/Reports/admin-basic";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Reports/GetAdminBasicReports.sql");
            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

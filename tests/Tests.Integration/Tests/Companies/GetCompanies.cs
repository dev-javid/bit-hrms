namespace Tests.Integration.Tests.Companies
{
    public class GetCompanies(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsSuperAdminAsync();
            await FeedDataAsync("Tests/Companies/GetCompanies.sql");
            using HttpResponseMessage response = await Client.GetAsync($"{Route}?page=1&limit=5");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

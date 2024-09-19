namespace Tests.Integration.Tests.Departments
{
    public class GetDeparments(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/departments?page=1&limit=10";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Departments/GetDeparments.sql");
            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

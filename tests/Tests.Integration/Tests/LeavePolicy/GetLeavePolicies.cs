namespace Tests.Integration.Tests.LeavePolicy
{
    public class GetLeavePolicies(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/leave-policy?page=1&limit=10";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/LeavePolicy/GetLeavePolicies.sql");
            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

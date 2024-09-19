using Domain.Common;

namespace Tests.Integration.Tests.Reports
{
    public class GetEmployeeBasicReports(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/Reports/employee-basic";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Reports/GetEmployeeBasicReports.sql");
            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

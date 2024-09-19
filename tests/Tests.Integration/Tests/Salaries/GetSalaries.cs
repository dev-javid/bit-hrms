using Xunit.Abstractions;

namespace Tests.Integration.Tests.Salaries
{
    public class GetSalaries(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/salaries";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_For_Supplied_Month_If_Month_Supplied()
        {
            // Arrange
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Salaries/GetSalaries.sql");

            // Act
            using HttpResponseMessage response = await Client.GetAsync($"{Route}?Month=6");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result.Ignore("items.[*].salaryId"));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Date_For_Current_Month_If_Month_Not_Supplied()
        {
            // Arrange
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Salaries/GetSalaries.sql");

            // Act
            using HttpResponseMessage response = await Client.GetAsync(Route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result.Ignore("items.[*].salaryId", "items.[*].disbursedOn", "items.[*].month", "items.[*].year"));
        }
    }
}

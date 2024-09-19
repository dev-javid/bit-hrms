namespace Tests.Integration.Tests.Employees
{
    public class GetEmployee(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/employees/3";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            // Arrange
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Employees/GetEmployee.sql");

            // Act
            using HttpResponseMessage response = await Client.GetAsync(Route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

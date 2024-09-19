namespace Tests.Integration.Tests.Departments
{
    public class GetDeparment(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/departments/1";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            // Arrange
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Departments/GetDeparment.sql");

            // Act
            using HttpResponseMessage response = await Client.GetAsync(Route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

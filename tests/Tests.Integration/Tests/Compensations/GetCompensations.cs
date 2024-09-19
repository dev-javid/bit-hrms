namespace Tests.Integration.Tests.Compensations
{
    public class GetCompensations(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/compensations";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_Of_Other_Employee_For_Company_Admin()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Compensations/GetCompensations.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?EmployeeId=2");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_Of_Own_Employee()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Compensations/GetCompensations.sql");

            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

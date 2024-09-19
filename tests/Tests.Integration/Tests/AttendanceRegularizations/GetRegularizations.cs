namespace Tests.Integration.Tests.AttendanceRegularizations
{
    public class GetRegularizations(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/attendance-regularizations";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_Of_Other_Employee_For_Company_Admin()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/AttendanceRegularizations/GetRegularizations.sql");

            using var response = await Client.GetAsync($"{Route}?EmployeeId=2");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Own_Employee_Data_For_Employee()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/AttendanceRegularizations/GetRegularizations.sql");

            using var response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

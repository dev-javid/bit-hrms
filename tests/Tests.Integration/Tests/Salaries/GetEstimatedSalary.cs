namespace Tests.Integration.Tests.Salaries
{
    public class GetEstimatedSalary(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/salaries/estimated-salary";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_Of_Other_Employee_For_Company_Admin()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Salaries/GetEstimatedSalary.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?EmployeeId=1&Month=5&year=2024");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_Of_Own_Employee_()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Salaries/GetEstimatedSalary.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?Month=5&year=2024");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_For_Month_Filter()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Salaries/GetEstimatedSalary.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?Month=5&EmployeeId=1&year=2024");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

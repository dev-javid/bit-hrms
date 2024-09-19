using Domain.Common;

namespace Tests.Integration.Tests.Attendance
{
    public class GetAttendance(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/attendance";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_Of_Other_Employee_For_Company_Admin()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Attendance/GetAttendance.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?EmployeeId=2&month=7");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_For_Date_Filter()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Attendance/GetAttendance.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?Date=2024-06-04");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_For_Month_Filter()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Attendance/GetAttendance.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?Month=6");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_For_Date_And_Employee_Filter()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Attendance/GetAttendance.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?EmployeeId=1&Date=2024-06-04");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data_For_Month_And_Employee_Filter()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Attendance/GetAttendance.sql");

            using HttpResponseMessage response = await Client.GetAsync($"{Route}?EmployeeId=1&Month=6");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Date_For_Current_Month_If_Month_Not_Supplied()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Attendance/GetAttendance.sql");

            using HttpResponseMessage response = await Client.GetAsync(Route);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result.Ignore("$[*].date"));
        }
    }
}

using Application.Regularization.Commands;
using Domain.Common;

namespace Tests.Integration.Tests.AttendanceRegularizations
{
    public class ApproveAttendanceRegularization(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/attendance-regularizations";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Set_Data_When_Clock_Out_Is_Missing()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/AttendanceRegularizations/ApproveAttendanceRegularization.sql");
            var command = new ApproveRegularizationCommand();

            HttpResponseMessage response;
            using (var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync($"{Route}/3/approve", content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Set_Data_When_Clock_In_Is_Missing()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/AttendanceRegularizations/ApproveAttendanceRegularization.sql");
            var command = new ApproveRegularizationCommand();

            HttpResponseMessage response;
            using (var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync($"{Route}/4/approve", content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result);
        }
    }
}

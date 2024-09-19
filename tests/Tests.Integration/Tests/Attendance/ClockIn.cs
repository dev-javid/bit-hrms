using Application.Attendance.Commands;
using Domain.Common;

namespace Tests.Integration.Tests.Attendance
{
    public class ClockIn(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/Attendance/clock-In";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Set_Data()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            await LoginAsEmployeeAsync();
            var command = new ClockInCommand();

            HttpResponseMessage response;
            using (var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result);
        }
    }
}

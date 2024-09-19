using Application.Attendance.Commands;

namespace Tests.Integration.Tests.Attendance
{
    public class ClockOut(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/attendance/clock-out";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Add_Data()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/Attendance/ClockOut.sql");
            var command = new ClockOutCommand();

            HttpResponseMessage response;
            using (var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result);
        }
    }
}

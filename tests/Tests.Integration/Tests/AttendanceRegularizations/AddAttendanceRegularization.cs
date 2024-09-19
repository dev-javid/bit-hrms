using Application.Regularization.Commands;

namespace Tests.Integration.Tests.AttendanceRegularizations
{
    public class AddAttendanceRegularization(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "api/attendance-regularizations";

        [Fact]
        public async Task Should_Return_Data()
        {
            await LoginAsEmployeeAsync();
            var command = new AddAttendanceRegularizationCommand
            {
                Date = new DateOnly(2024, 06, 22),
                ClockInTime = new TimeOnly(09, 00, 00),
                ClockOutTime = new TimeOnly(18, 00, 00),
                Reason = "sick-ness"
            };
            HttpResponseMessage response;
            using (var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Dispose();
        }
    }
}

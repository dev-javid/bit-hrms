using Application.EmployeeLeaves.Command;
using Bogus;

namespace Tests.Integration.Tests.EmployeeLeaves
{
    public class ApplyLeave(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "api/employee-leaves";

        [Fact]
        public async Task Should_Return_Data()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/EmployeeLeaves/ApplyLeave.sql");
            var command = new ApplyLeaveCommand
            {
                From = new DateOnly(2024, 06, 22),
                To = new DateOnly(2024, 06, 25),
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Dispose();
        }
    }
}

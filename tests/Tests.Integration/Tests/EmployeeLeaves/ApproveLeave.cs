using Application.EmployeeLeaves.Command;

namespace Tests.Integration.Tests.EmployeeLeaves
{
    public class ApproveLeave(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/employee-leaves/1/approve";

        [Fact]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/EmployeeLeaves/ApproveLeave.sql");
            var command = new ApproveLeaveCommand
            {
                EmployeeLeaveId = 1
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Dispose();
        }
    }
}

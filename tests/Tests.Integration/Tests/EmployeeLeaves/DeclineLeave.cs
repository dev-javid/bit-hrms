using Application.EmployeeLeaves.Command;

namespace Tests.Integration.Tests.EmployeeLeaves
{
    public class DeclineLeave(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/employee-leaves/1/decline";

        [Fact]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/EmployeeLeaves/DeclineLeave.sql");
            var command = new DeclineLeaveCommand
            {
                EmployeeLeaveId = 1,
                Remarks = "Remarks"
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

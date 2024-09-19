using Application.Users.Commands;

namespace Tests.Integration.Tests.Users
{
    public class AddUser(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/users";

        [Fact]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            var command = new AddUserCommand
            {
               Email = "test@example.com",
               Name = "Test",
               PhoneNumber = "1234567890",
               Role = "Employee"
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

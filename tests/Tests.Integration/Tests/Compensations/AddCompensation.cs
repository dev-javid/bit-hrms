using Application.Compensatios.Commands;

namespace Tests.Integration.Tests.Compensations
{
    public class AddCompensation(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "api/compensations";

        [Fact]
        public async Task Should_Add_Data()
        {
            await LoginAsCompanyAdminAsync();
            var command = new AddCompensationCommand
            {
                EmployeeId = 1,
                EffectiveFrom = new DateOnly(2024, 08, 22),
                Amount = 50,
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

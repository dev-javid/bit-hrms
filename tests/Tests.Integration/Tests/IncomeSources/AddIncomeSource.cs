using Application.IncomeSources.Commands;
using Bogus;

namespace Tests.Integration.Tests.IncomeSources;

public class AddIncomeSource(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task Should_add_data()
    {
        await LoginAsCompanyAdminAsync();
        var command = new AddIncomeSourceCommand
        {
            Name = new Faker().Random.String2(30),
            Description = new Faker().Random.String2(100)
        };
        HttpResponseMessage response;
        using (var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
        {
            response = await Client.PostAsync("/api/income-sources", content);
        }

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Dispose();
    }
}

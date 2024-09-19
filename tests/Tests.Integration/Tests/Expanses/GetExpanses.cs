namespace Tests.Integration.Tests.Expanses;

public class GetExpanses(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
{
    private const string Route = "api/expanses?page=1&limit=10";

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task Should_return_expanses()
    {
        await LoginAsCompanyAdminAsync();
        await FeedDataAsync("Tests/Expanses/GetExpanses.sql");

        HttpResponseMessage response = await Client.GetAsync(Route);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadAsStringAsync();
        Approvals.VerifyJson(result);
    }
}

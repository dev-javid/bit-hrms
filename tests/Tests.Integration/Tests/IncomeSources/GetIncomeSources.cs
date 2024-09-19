namespace Tests.Integration.Tests.IncomeSources;

public class GetIncomeSources(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
{
    private const string Route = "/api/income-sources?page=1&limit=10";

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task Should_Return_Income_Sources()
    {
        await LoginAsCompanyAdminAsync();
        await FeedDataAsync("Tests/IncomeSources/GetIncomeSources.sql");

        using HttpResponseMessage response = await Client.GetAsync(Route);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadAsStringAsync();
        Approvals.VerifyJson(result);
    }
}

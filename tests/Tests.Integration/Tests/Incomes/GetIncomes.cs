namespace Tests.Integration.Tests.Incomes;

public class GetIncomes(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
{
    private const string Route = "/api/incomes?page=1&limit=10";

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task Should_return_data()
    {
        await LoginAsCompanyAdminAsync();
        await FeedDataAsync("Tests/Incomes/GetIncomes.sql");

        HttpResponseMessage response = await Client.GetAsync(Route);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadAsStringAsync();
        Approvals.VerifyJson(result);
    }
}

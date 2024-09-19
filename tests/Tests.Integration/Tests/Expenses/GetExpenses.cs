namespace Tests.Integration.Tests.Expenses;

public class GetExpenses(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
{
    private const string Route = "api/expenses?page=1&limit=10";

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task Should_return_expenses()
    {
        await LoginAsCompanyAdminAsync();
        await FeedDataAsync("Tests/Expenses/GetExpenses.sql");

        HttpResponseMessage response = await Client.GetAsync(Route);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadAsStringAsync();
        Approvals.VerifyJson(result);
    }
}

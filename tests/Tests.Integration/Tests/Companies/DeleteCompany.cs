namespace Tests.Integration.Tests.Companies
{
    public class DeleteCompany(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies/1";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Delete_Company()
        {
            await LoginAsSuperAdminAsync();
            await FeedDataAsync("Tests/Companies/DeleteCompany.sql");

            using (HttpResponseMessage response = await Client.DeleteAsync(Route))
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            using (HttpResponseMessage response = await Client.GetAsync(Route))
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}

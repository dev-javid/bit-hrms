namespace Tests.Integration.Tests.Companies
{
    public class DeleteCompany(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Delete_Company()
        {
            await LoginAsSuperAdminAsync();
            await FeedDataAsync("Tests/Companies/DeleteCompany.sql");

            using (HttpResponseMessage response = await Client.DeleteAsync($"{Route}/1"))
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            using (HttpResponseMessage response = await Client.GetAsync($"{Route}?page=1&limit=5"))
            {
                var result = await response.Content.ReadAsStringAsync();
                Approvals.VerifyJson(result);
            }
        }
    }
}

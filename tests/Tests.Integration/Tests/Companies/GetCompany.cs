namespace Tests.Integration.Tests.Companies
{
    public class GetCompany(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies/999";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Get_Company()
        {
            await LoginAsSuperAdminAsync();
            using HttpResponseMessage response = await Client.GetAsync(Route);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

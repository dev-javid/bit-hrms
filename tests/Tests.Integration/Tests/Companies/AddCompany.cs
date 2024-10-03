namespace Tests.Integration.Tests.Companies
{
    public class AddCompany(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Add_Company()
        {
            await LoginAsSuperAdminAsync();

            var command = new
            {
                Name = "Company Name",
                AdministratorName = "Administrator Name",
                Address = "Some Random Place on the Planet Earth",
                Email = "company@example.com",
                PhoneNumber = "0123456789",
            };

            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result.Ignore("$.companyId"));
        }
    }
}

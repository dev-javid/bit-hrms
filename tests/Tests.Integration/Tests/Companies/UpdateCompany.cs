namespace Tests.Integration.Tests.Companies
{
    public class UpdateCompany(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies/999";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Update_Company()
        {
            await LoginAsSuperAdminAsync();
            var command = new
            {
                Name = "New Company Name",
                AdministratorName = "New Administrator Name",
                Address = "Some Other Random Place on the Planet Earth",
                PhoneNumber = "1234567890",
            };

            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result);
        }
    }
}

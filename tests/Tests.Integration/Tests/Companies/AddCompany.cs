namespace Tests.Integration.Tests.Companies
{
    public class AddCompany(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/companies";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Post_Company()
        {
            await LoginAsSuperAdminAsync();

            var command = new
            {
                Name = Faker.Random.String(3, 100),
                AdministratorName = Faker.Random.String(3, 100),
                Faker.Person.Email,
                PhoneNumber = Faker.Random.Number(10000000, 999999999).ToString(),
            };

            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            Approvals.VerifyJson(result.Ignore("*"));
        }
    }
}

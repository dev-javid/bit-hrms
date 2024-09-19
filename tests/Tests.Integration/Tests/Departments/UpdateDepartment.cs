namespace Tests.Integration.Tests.Departments
{
    public class UpdateDepartment(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/departments/1";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Departments/UpdateDepartment.sql");
            var command = new
            {
                Name = Faker.Random.String(3, 50)
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result.Ignore("$.name"));
        }
    }
}

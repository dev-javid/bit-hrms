using Application.JobTitles.Commands;

namespace Tests.Integration.Tests.JobTitles
{
    public class AddJobTitle(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/job-titles";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            var command = new AddJobTitleCommand
            {
                DepartmentId = 3000,
                Name = "Test",
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result.Ignore("$.id"));
        }
    }
}

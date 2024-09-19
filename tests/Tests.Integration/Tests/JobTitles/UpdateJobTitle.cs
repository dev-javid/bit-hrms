using Application.JobTitles.Commands;

namespace Tests.Integration.Tests.JobTitles
{
    public class UpdateJobTitle(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/job-titles/1";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/JobTitles/UpdateJobTitle.sql");
            var command = new UpdateJobTitleCommand
            {
                JobTitleId = 1,
                Name = "Test",
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result);
        }
    }
}

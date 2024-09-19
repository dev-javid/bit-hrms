using Application.Employees.Commands;

namespace Tests.Integration.Tests.Employees
{
    public class UpdateEmployeeDocumnet(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/employees/3/documents";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Employees/UpdateEmployeeDocumnet.sql");
            var command = new SetEmployeeDocumentCommand
            {
                EmployeeId = 3,
                Document = SampleFiles.Image,
                Type = "PAN"
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result.Ignore("$.documents[0].url"));
        }
    }
}

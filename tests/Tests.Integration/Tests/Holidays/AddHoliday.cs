using Application.Holidays.Commands;

namespace Tests.Integration.Tests.Holidays
{
    public class AddHoliday(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/holidays";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            var command = new AddHolidayCommand
            {
                Name = "Eid ul udha",
                Date = new DateOnly(2024, 06, 22),
                Optional = true,
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

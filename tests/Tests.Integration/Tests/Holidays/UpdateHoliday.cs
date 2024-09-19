using Application.Holidays.Commands;

namespace Tests.Integration.Tests.Holidays
{
    public class UpdateHoliday(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/holidays/1";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Holidays/UpdateHoliday.sql");
            var command = new UpdateHolidayCommand
            {
                HolidayId = 1,
                Name = "Test",
                Date = new DateOnly(2025, 09, 12),
                Optional = false,
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

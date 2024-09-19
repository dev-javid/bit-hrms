using Application.Salaries.Commands;
using Bogus;
using Microsoft.AspNetCore.Routing;

namespace Tests.Integration.Tests.Salaries
{
    public class AddSalary(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "api/salaries";

        [Fact]
        public async Task Should_Add_Salary_To_Employees_By_Company_Admin()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/Salaries/GetEstimatedSalary.sql");

            var command = new AddSalaryCommand
            {
                EmployeeId = 1,
                Month = 6,
                Year = 2024
            };

            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

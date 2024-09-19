using Application.Employees.Commands;

namespace Tests.Integration.Tests.Employees
{
    public class AddEmployee(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/employees";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            var command = new AddEmployeeCommand
            {
                DepartmentId = 3000,
                PersonalEmail = "abc@gmail.com",
                Aadhar = "123456789012",
                DateOfBirth = new DateOnly(2001, 06, 12),
                DateOfJoining = new DateOnly(2019, 06, 12),
                Address = "test",
                PAN = "GNCPS7000S",
                City = "test",
                PhoneNumber = "1234567890",
                JobTitleId = 4000,
                CompanyEmail = "abc@gmail.com",
                FatherName = "raza",
                FirstName = "mohd",
                LastName = "Saqib"
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result.Ignore("$.value.employeeId"));
        }
    }
}

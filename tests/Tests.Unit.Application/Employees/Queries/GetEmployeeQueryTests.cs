using Application.Employees.Queries;

namespace Tests.Unit.Application.Employees.Queries
{
    public class GetEmployeeQueryTests
    {
        [Fact]
        public async Task Should_Have_Error_When_EmployeeId_Is_Less_Then_Minimum_1()
        {
            //Arrange
            var command = new GetEmployeeQuery()
            {
                EmployeeId = new Faker().Random.Number(0)
            };

            var validator = new GetEmployeeQuery.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.EmployeeId)
                .WithErrorMessage("'Employee Id' must be greater than '0'.");
        }
    }
}

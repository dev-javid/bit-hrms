using Application.Salaries.Commands;

namespace Tests.Unit.Application.Salaries.Commands
{
    public class AddSalaryCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_EmployeeId_Is_Less_Then_Zero()
        {
            //Arrange
            var command = new AddSalaryCommand()
            {
                EmployeeId = new Faker().Random.Number(0),
                Year = new Faker().Random.Number(2000),
            };

            var validator = new AddSalaryCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.EmployeeId)
                .WithErrorMessage("'Employee Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Year_Is_Less_Then_2000()
        {
            //Arrange
            var command = new AddSalaryCommand()
            {
                EmployeeId = 1,
                Year = new Faker().Random.Number(-5000, 1999),
            };

            var validator = new AddSalaryCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Year)
                .WithErrorMessage("'Year' must be greater than '2000'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_valid()
        {
            //Arrange
            var command = new AddSalaryCommand()
            {
                EmployeeId = 1,
                Month = 1,
                Year = 2024
            };
            var validator = new AddSalaryCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

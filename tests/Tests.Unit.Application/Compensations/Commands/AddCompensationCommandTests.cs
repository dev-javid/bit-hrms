using Application.Compensatios.Commands;
using Application.Departments.Commands;

namespace Tests.Unit.Application.Compensations.Commands
{
    public class AddCompensationCommandTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Should_Have_Error_When_Employee_Id_Is_Zero(int employeeId)
        {
            //Arrange
            var command = new AddCompensationCommand
            {
                EmployeeId = employeeId,
                Amount = 20000,
                EffectiveFrom = new DateOnly(2024, 08, 22),
            };
            var validator = new AddCompensationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.EmployeeId)
               .WithErrorMessage("'Employee Id' must be greater than '0'.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Should_Have_Error_When_Amount_Is_Zero(int amount)
        {
            //Arrange
            var command = new AddCompensationCommand
            {
                EmployeeId = 1,
                Amount = amount,
                EffectiveFrom = new DateOnly(2024, 08, 22),
            };
            var validator = new AddCompensationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.Amount)
               .WithErrorMessage("'Amount' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_valid()
        {
            //Arrange
            var command = new AddCompensationCommand()
            {
                EmployeeId = 1,
                Amount = 1000,
                EffectiveFrom = new DateOnly(2024, 08, 22),
            };
            var validator = new AddCompensationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

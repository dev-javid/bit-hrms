using System.Globalization;
using Application.EmployeeLeaves.Command;

namespace Tests.Unit.Application.EmployeeLeaves.Commands
{
    public class ApplyLeaveCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_From_Date_Is_Empty()
        {
            //Arrange
            var command = new ApplyLeaveCommand()
            {
                From = default,
                To = DateOnly.ParseExact("2024-05-31", "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };

            var validator = new ApplyLeaveCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                 .ShouldHaveValidationErrorFor(x => x.From)
                 .WithErrorMessage("'From' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_To_Date_Is_Empty()
        {
            //Arrange
            var command = new ApplyLeaveCommand()
            {
                From = DateOnly.ParseExact("2024-05-14", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                To = default
            };

            var validator = new ApplyLeaveCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                 .ShouldHaveValidationErrorFor(x => x.To)
                 .WithErrorMessage("'To' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_To_Date_Is_Earlier_Than_From_Date()
        {
            // Arrange
            var command = new ApplyLeaveCommand()
            {
                From = new Faker().Date.FutureDateOnly(),
                To = new Faker().Date.PastDateOnly()
            };

            var validator = new ApplyLeaveCommand.Validator();

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x)
              .WithErrorMessage("'From' date must be earlier than or equal to the 'To' date.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_valid()
        {
            //Arrange
            var command = new ApplyLeaveCommand()
            {
                From = DateOnly.ParseExact("2024-05-14", "yyyy-mm-dd", CultureInfo.InvariantCulture),
                To = DateOnly.ParseExact("2024-05-31", "yyyy-mm-dd", CultureInfo.InvariantCulture)
            };

            var validator = new ApplyLeaveCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

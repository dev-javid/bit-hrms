using System.Globalization;
using Application.Regularization.Commands;

namespace Tests.Unit.Application.Regularization.Commands
{
    public class RequestAttendanceRegularizationCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_Date_Is_Empty()
        {
            //Arrange
            var command = new AddAttendanceRegularizationCommand()
            {
                ClockOutTime = new TimeOnly(21, 37, 45),
                ClockInTime = new TimeOnly(5, 37, 45),
                Date = default,
                Reason = "no reason",
            };

            var validator = new AddAttendanceRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                 .ShouldHaveValidationErrorFor(x => x.Date)
                 .WithErrorMessage("'Date' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_ClockIn_Is_Empty()
        {
            //Arrange
            var command = new AddAttendanceRegularizationCommand()
            {
                ClockInTime = default,
                ClockOutTime = new TimeOnly(5, 37, 45),
                Date = DateOnly.ParseExact("2024-05-31", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Reason = "no reason"
            };

            var validator = new AddAttendanceRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                 .ShouldHaveValidationErrorFor(x => x.ClockInTime)
                 .WithErrorMessage("'Clock In Time' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Clockout_Is_Empty()
        {
            //Arrange
            var command = new AddAttendanceRegularizationCommand()
            {
                ClockOutTime = default,
                ClockInTime = new TimeOnly(5, 37, 45),
                Date = DateOnly.ParseExact("2024-05-31", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Reason = "no reason"
            };

            var validator = new AddAttendanceRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                 .ShouldHaveValidationErrorFor(x => x.ClockOutTime)
                 .WithErrorMessage("'Clock Out Time' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Reason_Is_Empty()
        {
            //Arrange
            var command = new AddAttendanceRegularizationCommand()
            {
                ClockInTime = new TimeOnly(5, 37, 45),
                ClockOutTime = new TimeOnly(5, 37, 45),
                Date = DateOnly.ParseExact("2024-05-31", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Reason = ""
            };

            var validator = new AddAttendanceRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                 .ShouldHaveValidationErrorFor(x => x.Reason)
                 .WithErrorMessage("'Reason' must not be empty.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new AddAttendanceRegularizationCommand()
            {
                ClockInTime = new TimeOnly(5, 37, 45),
                ClockOutTime = new TimeOnly(15, 37, 45),
                Date = DateOnly.ParseExact("2024-05-31", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Reason = "no reason"
            };

            var validator = new AddAttendanceRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldNotHaveAnyValidationErrors();
        }
    }
}

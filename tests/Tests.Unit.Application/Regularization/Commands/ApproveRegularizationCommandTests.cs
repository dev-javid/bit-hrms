using Application.Regularization.Commands;

namespace Tests.Unit.Application.Regularization.Commands
{
    public class ApproveRegularizationCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_Regularization_Id_Is_Less_Then_Zero()
        {
            //Arrange
            var command = new ApproveRegularizationCommand()
            {
                AttendanceRegularizationId = new Faker().Random.Number(0),
            };

            var validator = new ApproveRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.AttendanceRegularizationId)
                .WithErrorMessage("'Attendance Regularization Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_valid()
        {
            //Arrange
            var command = new ApproveRegularizationCommand()
            {
                AttendanceRegularizationId = 1,
            };
            var validator = new ApproveRegularizationCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

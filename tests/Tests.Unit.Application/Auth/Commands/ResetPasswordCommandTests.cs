using Application.Auth.Commands;

namespace Tests.Unit.Application.Auth.Commands
{
    public class ResetPasswordCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_UserId_Is_Less_than_Or_Equal_To_Zero()
        {
            //Arrange
            var command = new ResetPasswordCommand()
            {
                UserId = new Faker().Random.Number(0),
                Token = new Faker().Random.String2(2),
                Password = new Faker().Random.String2(3)
            };

            var validator = new ResetPasswordCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.UserId)
                .WithErrorMessage("'User Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Token_Is_Empty()
        {
            //Arrange
            var command = new ResetPasswordCommand()
            {
                UserId = new Faker().Random.Number(2),
                Token = new Faker().Random.String2(0),
                Password = new Faker().Random.String2(3)
            };

            var validator = new ResetPasswordCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Token)
                .WithErrorMessage("'Token' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Password_Is_Empty()
        {
            //Arrange
            var command = new ResetPasswordCommand()
            {
                UserId = new Faker().Random.Number(2),
                Token = new Faker().Random.String2(3),
                Password = new Faker().Random.String2(0)
            };

            var validator = new ResetPasswordCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("'Password' must not be empty.");
        }
    }
}

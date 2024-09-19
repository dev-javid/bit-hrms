using Application.Departments.Commands;

namespace Tests.Unit.Application.Departments.Commands
{
    public class AddDepartmentCommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Name_Is_Empty(string name)
        {
            //Arrange
            var command = new AddDepartmentCommand
            {
                Name = name
            };
            var validator = new AddDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("'Name' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Is_Shorter_Then_Minimum_Length()
        {
            //Arrange
            var command = new AddDepartmentCommand()
            {
                Name = new Faker().Random.String2(1)
            };
            var validator = new AddDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be at least 2 characters. You entered 1 characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new AddDepartmentCommand()
            {
                Name = new Faker().Random.String2(101)
            };
            var validator = new AddDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 101 characters.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_valid()
        {
            //Arrange
            var command = new AddDepartmentCommand()
            {
                Name = new Faker().Random.String2(2, 50),
            };
            var validator = new AddDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

using Application.JobTitles.Commands;

namespace Tests.Unit.Application.JobTitles.Commands
{
    public class AddJobTitleCommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Name_Is_Empty(string name)
        {
            //Arrange
            var command = new AddJobTitleCommand()
            {
                Name = name,
                DepartmentId = new Faker().Random.Int(1, int.MaxValue)
            };
            var validator = new AddJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("'Name' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new AddJobTitleCommand()
            {
                Name = new Faker().Random.String2(51),
                DepartmentId = new Faker().Random.Int(1, int.MaxValue)
            };
            var validator = new AddJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_DepartmentId_Is_Zero_Or_Negative()
        {
            //Arrange
            var command = new AddJobTitleCommand
            {
                DepartmentId = new Faker().Random.Int(int.MinValue, 0),
                Name = new Faker().Random.String2(1, 50)
            };
            var validator = new AddJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.DepartmentId)
               .WithErrorMessage("'Department Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new AddJobTitleCommand()
            {
                Name = new Faker().Random.String2(1, 50),
                DepartmentId = new Faker().Random.Int(1, int.MaxValue)
            };
            var validator = new AddJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

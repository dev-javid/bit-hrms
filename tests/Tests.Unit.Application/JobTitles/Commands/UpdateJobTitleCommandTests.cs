using Application.JobTitles.Commands;

namespace Tests.Unit.Application.JobTitles.Commands
{
    public class UpdateJobTitleCommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Name_Is_Empty(string name)
        {
            //Arrange
            var command = new UpdateJobTitleCommand()
            {
                Name = name,
                JobTitleId = new Faker().Random.Int(1, int.MaxValue)
            };
            var validator = new UpdateJobTitleCommand.Validator();

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
            var command = new UpdateJobTitleCommand()
            {
                Name = new Faker().Random.String2(51),
                JobTitleId = new Faker().Random.Int(1, int.MaxValue)
            };
            var validator = new UpdateJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_JobTitle_Id_Is_Zero_Or_Negative()
        {
            //Arrange
            var command = new UpdateJobTitleCommand
            {
                JobTitleId = new Faker().Random.Int(int.MinValue, 0),
                Name = new Faker().Random.String2(8)
            };
            var validator = new UpdateJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.JobTitleId)
               .WithErrorMessage("'Job Title Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new UpdateJobTitleCommand()
            {
                Name = new Faker().Random.String2(8),
                JobTitleId = new Faker().Random.Int(1, int.MaxValue)
            };
            var validator = new UpdateJobTitleCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

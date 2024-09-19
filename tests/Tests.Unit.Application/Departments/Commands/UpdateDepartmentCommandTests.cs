using Application.Departments.Commands;

namespace Tests.Unit.Application.Departments.Commands
{
    public class UpdateDepartmentCommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Name_Is_Empty(string name)
        {
            //Arrange
            var command = new UpdateDepartmentCommand
            {
                DepartmentId = 1,
                Name = name
            };
            var validator = new UpdateDepartmentCommand.Validator();

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
            var command = new UpdateDepartmentCommand()
            {
                DepartmentId = 1,
                Name = new Faker().Random.String2(2)
            };
            var validator = new UpdateDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be at least 3 characters. You entered 2 characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new UpdateDepartmentCommand()
            {
                DepartmentId = 1,
                Name = new Faker().Random.String2(51)
            };
            var validator = new UpdateDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Should_Have_Error_When_DepartmentId_Is_Zero(int departmentId)
        {
            //Arrange
            var command = new UpdateDepartmentCommand
            {
                DepartmentId = departmentId,
                Name = "aaaa"
            };
            var validator = new UpdateDepartmentCommand.Validator();

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
            var command = new UpdateDepartmentCommand()
            {
                DepartmentId = new Faker().Random.Int(1),
                Name = new Faker().Random.String2(3),
            };
            var validator = new UpdateDepartmentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

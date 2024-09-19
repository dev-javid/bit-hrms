using Application.Employees.Commands;

namespace Tests.Unit.Application.Employees.Commands
{
    public class SetEmployeeDocumentCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_EmployeeId_Is_Less_Then_Zero()
        {
            //Arrange
            var command = new SetEmployeeDocumentCommand()
            {
                EmployeeId = new Faker().Random.Number(0),
                Type = new Faker().Random.String2(5),
                Document = new Faker().Random.String2(5),
            };

            var validator = new SetEmployeeDocumentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.EmployeeId)
                .WithErrorMessage("'Employee Id' must be greater than '0'.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Type_Is_Empty(string type)
        {
            // Arrange
            var command = new SetEmployeeDocumentCommand()
            {
                EmployeeId = new Faker().Random.Number(1, 1000),
                Type = type,
                Document = new Faker().Random.String2(5)
            };

            var validator = new SetEmployeeDocumentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Type)
                .WithErrorMessage("'Type' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Document_Is_Empty(string document)
        {
            // Arrange
            var command = new SetEmployeeDocumentCommand()
            {
                EmployeeId = new Faker().Random.Number(1, 1000),
                Type = new Faker().Random.String2(5),
                Document = document,
            };

            var validator = new SetEmployeeDocumentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Document)
                .WithErrorMessage("'Document' must not be empty.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_valid()
        {
            //Arrange
            var command = new SetEmployeeDocumentCommand()
            {
                EmployeeId = 1,
                Type = new Faker().Random.String2(5),
                Document = new Faker().Random.String2(5),
            };
            var validator = new SetEmployeeDocumentCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

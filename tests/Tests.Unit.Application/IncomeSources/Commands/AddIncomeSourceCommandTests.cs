using Application.IncomeSources.Commands;

namespace Tests.Unit.Application.IncomeSources.Commands;

public class AddIncomeSourceCommandTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_have_error_when_name_is_empty(string name)
    {
        var command = new AddIncomeSourceCommand
        {
            Name = name,
            Description = "Description"
        };

        var validator = new AddIncomeSourceCommand.Validator();
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("'Name' must not be empty.");
    }

    [Fact]
    public void Should_have_error_when_name_exceeds_maximum_length()
    {
        var command = new AddIncomeSourceCommand
        {
            Name = new Faker().Random.String2(52),
            Description = "Description"
        };

        var validator = new AddIncomeSourceCommand.Validator();
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 52 characters.");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_have_error_when_description_is_empty(string description)
    {
        var command = new AddIncomeSourceCommand
        {
            Name = "Name",
            Description = description
        };

        var validator = new AddIncomeSourceCommand.Validator();
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("'Description' must not be empty.");
    }

    [Fact]
    public void Should_not_have_any_error_when_request_is_valid()
    {
        var command = new AddIncomeSourceCommand
        {
            Name = "Name",
            Description = "Description"
        };

        var validator = new AddIncomeSourceCommand.Validator();
        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}

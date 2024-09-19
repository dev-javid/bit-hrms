using Application.Holidays.Commands;

namespace Tests.Unit.Application.Holidays.Commands
{
    public class AddHolidayCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new AddHolidayCommand()
            {
                Name = "",
                Date = DateOnly.FromDateTime(DateTime.Now),
                Optional = false,
            };
            var validator = new AddHolidayCommand.Validator();
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("'Name' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new AddHolidayCommand()
            {
                Name = new Faker().Random.String2(51),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Optional = false,
            };
            var validator = new AddHolidayCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
        }
    }
}

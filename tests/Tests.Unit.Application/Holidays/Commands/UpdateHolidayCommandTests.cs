using Application.Holidays.Commands;

namespace Tests.Unit.Application.Holidays.Commands
{
    public class UpdateHolidayCommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Name_Is_Empty(string name)
        {
            //Arrange
            var command = new UpdateHolidayCommand()
            {
                Name = name,
                Optional = new Faker().Random.Bool(),
                Date = new Faker().Date.FutureDateOnly()
            };
            var validator = new UpdateHolidayCommand.Validator();

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
            var command = new UpdateHolidayCommand()
            {
                Name = new Faker().Random.String2(51),
                Date = new Faker().Date.FutureDateOnly(),
                Optional = new Faker().Random.Bool()
            };
            var validator = new UpdateHolidayCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_HolidayId_Is_Zero_Or_Negative()
        {
            //Arrange
            var command = new UpdateHolidayCommand
            {
                HolidayId = new Faker().Random.Int(int.MinValue, 0),
                Name = new Faker().Random.String2(8),
                Date = new Faker().Date.FutureDateOnly(),
                Optional = new Faker().Random.Bool()
            };
            var validator = new UpdateHolidayCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.HolidayId)
               .WithErrorMessage("'Holiday Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new UpdateHolidayCommand()
            {
                HolidayId = new Faker().Random.Int(1, int.MaxValue),
                Name = new Faker().Random.String2(1, 50),
                Date = new Faker().Date.FutureDateOnly(),
                Optional = new Faker().Random.Bool()
            };
            var validator = new UpdateHolidayCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

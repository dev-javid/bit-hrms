using Application.Companies.Commands;

namespace Tests.Unit.Application.Companies.Commands
{
    public class AddCompanyCommandTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Name_Is_Empty(string name)
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Name = name
            };
            var validator = new AddCompanyCommand.Validator();

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
            var command = new AddCompanyCommand()
            {
                Name = new Faker().Random.String2(2)
            };
            var validator = new AddCompanyCommand.Validator();

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
            var command = new AddCompanyCommand()
            {
                Name = new Faker().Random.String2(101)
            };
            var validator = new AddCompanyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("The length of 'Name' must be 100 characters or fewer. You entered 101 characters.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Email_Is_Empty(string email)
        {
            var command = new AddCompanyCommand()
            {
                Email = email
            };

            var validator = new AddCompanyCommand.Validator();
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("'Email' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_PhoneNumber_Is_Empty(string phoneNumber)
        {
            var command = new AddCompanyCommand()
            {
                PhoneNumber = phoneNumber
            };

            var validator = new AddCompanyCommand.Validator();
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("'Phone Number' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_AdministratorName_Is_Empty(string administratorName)
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                AdministratorName = administratorName
            };
            var validator = new AddCompanyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.AdministratorName)
                .WithErrorMessage("'Administrator Name' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_AdministratorName_Is_Shorter_Then_Minimum_Length()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                AdministratorName = new Faker().Random.String2(2)
            };
            var validator = new AddCompanyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result

                .ShouldHaveValidationErrorFor(x => x.AdministratorName)
                .WithErrorMessage("The length of 'Administrator Name' must be at least 3 characters. You entered 2 characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_AdministratorName_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                AdministratorName = new Faker().Random.String2(101)
            };
            var validator = new AddCompanyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.AdministratorName)
                .WithErrorMessage("The length of 'Administrator Name' must be 100 characters or fewer. You entered 101 characters.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Name = new Faker().Random.String2(3),
                Email = "admin@example.com",
                AdministratorName = new Faker().Random.String2(100),
                FinancialMonth = 1,
                PhoneNumber = "9876543210"
            };
            var validator = new AddCompanyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

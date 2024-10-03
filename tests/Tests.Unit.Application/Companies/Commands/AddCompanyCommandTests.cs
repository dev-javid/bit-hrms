using Application.Common.Abstract;
using Application.Companies.Commands;

namespace Tests.Unit.Application.Companies.Commands
{
    public class AddCompanyCommandTests
    {
        private readonly IDbContext _dbContext = Substitute.For<IDbContext>();

        public AddCompanyCommandTests()
        {
            var user = User.Create("existing.user@example.com".ToValueObject<Email>(), "1234567890".ToValueObject<PhoneNumber>());
            var dbSetData = new List<User> { user }
                .AsQueryable()
                .BuildMockDbSet();

            _dbContext.Users.Returns(dbSetData);
        }

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
            var validator = new AddCompanyCommand.Validator(_dbContext);

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
                Name = new Faker().Random.String2(1, 2)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage($"The length of 'Name' must be at least 3 characters. You entered {command.Name.Length} characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Name = new Faker().Random.String2(101, 1000)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage($"The length of 'Name' must be 100 characters or fewer. You entered {command.Name.Length} characters.");
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

            var validator = new AddCompanyCommand.Validator(_dbContext);
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("'Email' must not be empty.");
        }

        [Theory]
        [InlineData("existing.user@example.com")]
        [InlineData("EXISTING.USER@EXAMPLE.COM")]
        public async Task Should_Have_Error_When_Email_Exists(string email)
        {
            var command = new AddCompanyCommand()
            {
                Email = email
            };

            var validator = new AddCompanyCommand.Validator(_dbContext);
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage($"Email '${email}' is already in use.");
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

            var validator = new AddCompanyCommand.Validator(_dbContext);
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
            var validator = new AddCompanyCommand.Validator(_dbContext);

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
                AdministratorName = new Faker().Random.String2(1, 2)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result

                .ShouldHaveValidationErrorFor(x => x.AdministratorName)
                .WithErrorMessage($"The length of 'Administrator Name' must be at least 3 characters. You entered {command.AdministratorName.Length} characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_AdministratorName_Exceeds_Maximum_Length()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                AdministratorName = new Faker().Random.String2(101, 1000)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.AdministratorName)
                .WithErrorMessage($"The length of 'Administrator Name' must be 100 characters or fewer. You entered {command.AdministratorName.Length} characters.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Address_Is_Empty(string address)
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Address = address
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Address)
                .WithErrorMessage("'Address' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Address_Is_Less_Than_Minimum_Length()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Address = new Faker().Random.String(0, 29)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Address)
                .WithErrorMessage($"The length of 'Address' must be at least 30 characters. You entered {command.Address.Length} characters.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Address_Is_More_Than_Maximum_Length()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Address = new Faker().Random.String(201, 1000)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Address)
                .WithErrorMessage($"The length of 'Address' must be 200 characters or fewer. You entered {command.Address.Length} characters.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new AddCompanyCommand()
            {
                Name = new Faker().Random.String(3, 100),
                Email = "admin@example.com",
                AdministratorName = new Faker().Random.String(3, 100),
                PhoneNumber = "9876543210",
                Address = new Faker().Random.String(30, 200)
            };
            var validator = new AddCompanyCommand.Validator(_dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

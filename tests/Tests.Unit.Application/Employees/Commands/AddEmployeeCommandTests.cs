using Application.Common.Abstract;
using Application.Employees.Commands;
using Domain.Common.ValueObjects;
using Domain.Identity;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace Tests.Unit.Application.Employees.Commands
{
    public class AddEmployeeCommandTests
    {
        private readonly IDbContext dbContext = Substitute.For<IDbContext>();

        [Fact]
        public async Task Should_Have_Error_When_JobTitle_Id_Is_Zero_Or_Negative()
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand
            {
                JobTitleId = new Faker().Random.Int(int.MinValue, 0)
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.JobTitleId)
               .WithErrorMessage("'Job Title Id' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Have_Error_When_DepartmentId_Is_Zero_Or_Negative()
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand
            {
                DepartmentId = new Faker().Random.Int(int.MinValue, 0)
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
               .ShouldHaveValidationErrorFor(x => x.DepartmentId)
               .WithErrorMessage("'Department Id' must be greater than '0'.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_First_Name_Is_Empty(string firstName)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                FirstName = firstName
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("'First Name' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Fathers_Name_Is_Empty(string fatherName)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                FatherName = fatherName
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.FatherName)
                .WithErrorMessage("'Father Name' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Adress_Is_Empty(string adress)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                Address = adress
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Address)
                .WithErrorMessage("'Address' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Last_Name_Is_Empty(string lastName)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                LastName = lastName
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("'Last Name' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Phone_number_Is_Empty(string phoneNumber)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                PhoneNumber = phoneNumber
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("'Phone Number' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Company_Email_Is_Empty(string companyEmail)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                CompanyEmail = companyEmail
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.CompanyEmail)
                .WithErrorMessage("'Company Email' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Personal_Email_Is_Empty(string personalEmail)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                PersonalEmail = personalEmail
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.PersonalEmail)
                .WithErrorMessage("'Personal Email' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_City_Is_Empty(string city)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                City = city
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.City)
                .WithErrorMessage("'City' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_PAN_Is_Empty(string pan)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                PAN = pan
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.PAN)
                .WithErrorMessage("'PAN' must not be empty.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Have_Error_When_Aadhar_Is_Empty(string aadhar)
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                Aadhar = aadhar
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Aadhar)
                .WithErrorMessage("'Aadhar' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Date_Of_Birth_Is_empty()
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                DateOfBirth = default
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                .WithErrorMessage("'Date Of Birth' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Date_Of_Joining_Is_empty()
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                DateOfJoining = default
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.DateOfJoining)
                .WithErrorMessage("'Date Of Joining' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Phone_Number_Already_Exists()
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                PhoneNumber = "9906121212"
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("The phone number is already associated with an existing user.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            SetupUserData();

            var command = new AddEmployeeCommand()
            {
                FatherName = new Faker().Random.String2(8),
                LastName = new Faker().Random.String2(7),
                FirstName = new Faker().Random.String2(8),
                City = new Faker().Random.String2(8),
                CompanyEmail = new Faker().Random.String2(20),
                Address = new Faker().Random.String2(8),
                Aadhar = new Faker().Random.String2(16),
                PAN = new Faker().Random.String2(10),
                PersonalEmail = new Faker().Random.String2(20),
                PhoneNumber = new Faker().Random.String2(21),
                DepartmentId = new Faker().Random.Int(1, int.MaxValue),
                JobTitleId = new Faker().Random.Int(1, int.MaxValue),
                DateOfBirth = new Faker().Date.PastDateOnly(),
                DateOfJoining = new Faker().Date.PastDateOnly()
            };
            var validator = new AddEmployeeCommand.Validator(dbContext);

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldNotHaveAnyValidationErrors();
        }

        private void SetupUserData()
        {
            var user = new List<User>
            {
                User.Create(Email.From("Test@example.com"), PhoneNumber.From("9906121212"))
            }.AsQueryable();

            var dbSetData = user.BuildMockDbSet();
            dbContext.Users.Returns(dbSetData);
        }
    }
}

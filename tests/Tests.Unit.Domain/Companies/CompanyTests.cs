namespace Tests.Unit.Domain.Companies
{
    public class CompanyTests
    {
        private readonly Faker _faker = new();
        private readonly string _departmentName = "Enginnering";
        private readonly Company _company;

        public CompanyTests()
        {
            _company = Company.Create(
                _faker.Company.CompanyName(),
                _faker.Internet.Email().ToValueObject<Email>(),
                "1234567890".ToValueObject<PhoneNumber>(),
                _faker.Name.FullName(),
                _faker.Address.FullAddress());
        }

        [Fact]
        public void AddDepartment_Should_Throw_Exception_When_Name_Already_Exists()
        {
            //Arrange
            _company.AddDepartment(_departmentName);

            //Act
            Action action = () => _company.AddDepartment(_departmentName);

            //Assert
            action.Should().Throw<CustomException>()
                .WithMessage("Department with same name already exists.")
                .And
                .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void UpdateDepartment_Should_Throw_Exception_When_Department_Not_Found()
        {
            //Arrange

            //Act
            Action action = () => _company.UpdateDepartment(_faker.Random.Number(0, 10), _departmentName);

            //Assert
            action.Should().Throw<CustomException>()
                .WithMessage("Department not found.")
                .And
                .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.NotFound);
        }
    }
}

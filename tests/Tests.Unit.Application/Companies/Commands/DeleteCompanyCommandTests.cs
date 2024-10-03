using Application.Companies.Commands;

namespace Tests.Unit.Application.Companies.Commands
{
    public class DeleteCompanyCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_Name_Is_Less_Than_Or_Equal_To_Zero()
        {
            //Arrange
            var command = new DeleteCompanyCommand()
            {
                CompanyId = new Faker().Random.Number(0)
            };
            var validator = new DeleteCompanyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.CompanyId)
                .WithErrorMessage("'Company Id' must be greater than '0'.");
        }
    }
}

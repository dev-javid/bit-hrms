using Application.Compensatios.Query;

namespace Tests.Unit.Application.Compensations.Queries
{
    public class GetCompensationsQueryTests
    {
        [Fact]
        public async Task Should_Have_Error_When_EmployeeId_Is_Less_Then_0()
        {
            //Arrange
            var command = new GetCompensationsQuery()
            {
                EmployeeId = new Faker().Random.Number(0)
            };

            var validator = new GetCompensationsQuery.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.EmployeeId)
                .WithErrorMessage("'Employee Id' must be greater than '0'.");
        }
    }
}

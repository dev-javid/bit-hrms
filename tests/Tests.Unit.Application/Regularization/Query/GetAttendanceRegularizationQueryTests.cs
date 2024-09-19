using Application.Regularization.Queries;

namespace Tests.Unit.Application.Regularization.Query;

public class GetAttendanceRegularizationQueryTests
{
    [Fact]
    public async Task Should_Have_Error_When_EmployeeId_Is_Less_Then_0()
    {
        //Arrange
        var command = new GetRegularizationsQuery()
        {
            EmployeeId = new Faker().Random.Number(0)
        };

        var validator = new GetRegularizationsQuery.Validator();

        //Act
        var result = await validator.TestValidateAsync(command);

        //Assert
        result
            .ShouldHaveValidationErrorFor(x => x.EmployeeId)
            .WithErrorMessage("'Employee Id' must be greater than '0'.");
    }
}

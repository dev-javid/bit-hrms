using Application.LeavePolicies.Commands.SetLeavePolicy;

namespace Tests.Unit.Application.LeavePolicies.Commands.SetLeavePolicies
{
    public class SetLeavePolicyCommandTests
    {
        [Fact]
        public async Task Should_Have_Error_When_CasualLeaves_Is_Less_Then_Or_Equal_Zero()
        {
            //Arrange
            var command = new SetLeavePolicyCommand()
            {
                CasualLeaves = new Faker().Random.Int(int.MinValue, 0),
                Holidays = new Faker().Random.Int(1, int.MaxValue),
                EarnedLeavesPerMonth = new Faker().Random.Double(0.001, double.MaxValue)
            };

            var validator = new SetLeavePolicyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.CasualLeaves)
                .WithErrorMessage("'Casual Leaves' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Holidays_Is_Less_Then_Or_Equal_Zero()
        {
            //Arrange
            var command = new SetLeavePolicyCommand()
            {
                CasualLeaves = new Faker().Random.Int(1, int.MaxValue),
                Holidays = new Faker().Random.Int(int.MinValue, 0),
                EarnedLeavesPerMonth = new Faker().Random.Double(3, 4)
            };

            var validator = new SetLeavePolicyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.Holidays)
                .WithErrorMessage("'Holidays' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Have_Error_When_EarnedLeavesPerMonth_Is_Less_Then_Or_Equal_Zero()
        {
            //Arrange
            var command = new SetLeavePolicyCommand()
            {
                CasualLeaves = new Faker().Random.Int(int.MinValue, int.MaxValue),
                Holidays = new Faker().Random.Int(int.MinValue, int.MaxValue),
                EarnedLeavesPerMonth = new Faker().Random.Double(double.MinValue, 0)
            };

            var validator = new SetLeavePolicyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldHaveValidationErrorFor(x => x.EarnedLeavesPerMonth)
                .WithErrorMessage("'Earned Leaves Per Month' must be greater than '0'.");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Request_Is_Valid()
        {
            //Arrange
            var command = new SetLeavePolicyCommand()
            {
                CasualLeaves = new Faker().Random.Int(1, int.MaxValue),
                Holidays = new Faker().Random.Int(1, int.MaxValue),
                EarnedLeavesPerMonth = new Faker().Random.Double(0.001, double.MaxValue)
            };

            var validator = new SetLeavePolicyCommand.Validator();

            //Act
            var result = await validator.TestValidateAsync(command);

            //Assert
            result
                .ShouldNotHaveAnyValidationErrors();
        }
    }
}

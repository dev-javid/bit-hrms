using Application.Auth.Commands;
using Application.Common.Abstract;
using Domain.Common.ValueObjects;
using Domain.Identity;
using NSubstitute;

namespace Tests.Unit.Application.Auth.Commands
{
    public class RefreshTokenCommandTests
    {
        private readonly IIdentityService identityService = Substitute.For<IIdentityService>();
        private readonly IJwtService jwtService = Substitute.For<IJwtService>();

        public RefreshTokenCommandTests()
        {
            var email = new Faker().Person.Email.ToValueObject<Email>();

            jwtService.ExtractEmailFromToken(Arg.Any<string>()).Returns(email);
            identityService.GetUserAsync(Arg.Any<Email>()).Returns(User.Create(email, "9876543210".ToValueObject<PhoneNumber>()));
            jwtService.ValidateRefreshTokenAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);
        }

        [Fact]
        public async Task Should_Have_Error_When_RefreshToken_Is_Empty()
        {
            var command = new RefreshTokenCommand()
            {
                RefreshToken = "",
                AccessToken = "abc"
            };
            var validator = new RefreshTokenCommand.Validator(identityService, jwtService);
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.RefreshToken)
                .WithErrorMessage("'Refresh Token' must not be empty.");
        }

        [Fact]
        public async Task Should_Have_Error_When_AccessToken_Is_Empty()
        {
            var command = new RefreshTokenCommand()
            {
                RefreshToken = "abc",
                AccessToken = ""
            };
            var validator = new RefreshTokenCommand.Validator(identityService, jwtService);
            var result = await validator.TestValidateAsync(command);
            result
                .ShouldHaveValidationErrorFor(x => x.AccessToken)
                .WithErrorMessage("'Access Token' must not be empty.");
        }
    }
}

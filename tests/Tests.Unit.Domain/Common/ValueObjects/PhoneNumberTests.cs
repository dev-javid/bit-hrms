namespace Tests.Unit.Domain.Common.ValueObjects
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData("1234567890")]
        [InlineData("9876543210")]
        public void From_ValidPhoneNumber_ReturnsPhoneNumber(string phoneNumber)
        {
            // Act
            var result = PhoneNumber.From(phoneNumber);

            // Assert
            result.Value.Should().Be(phoneNumber);
        }

        [Theory]
        [InlineData("12345678")] // Too short
        [InlineData("123456789012345678901")] // Too long
        [InlineData("abc")] // Contains non-numeric characters
        public void From_InvalidPhoneNumber_ThrowsException(string phoneNumber)
        {
            // Act & Assert
            FluentActions.Invoking(() => PhoneNumber.From(phoneNumber)).Should().Throw<CustomException>();
        }

        [Fact]
        public void Equals_TwoEqualPhoneNumbers_ReturnsTrue()
        {
            // Arrange
            var phoneNumber1 = PhoneNumber.From("1234567890");
            var phoneNumber2 = PhoneNumber.From("1234567890");

            // Act
            var result = phoneNumber1.Equals(phoneNumber2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_TwoDifferentPhoneNumbers_ReturnsFalse()
        {
            // Arrange
            var phoneNumber1 = PhoneNumber.From("1234567890");
            var phoneNumber2 = PhoneNumber.From("9876543210");

            // Act
            var result = phoneNumber1.Equals(phoneNumber2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

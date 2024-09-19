namespace Tests.Unit.Domain.Common.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("test@example.com")]
        [InlineData("another.email@example.co.uk")]
        public void From_ValidEmail_ReturnsEmail(string email)
        {
            // Act
            var result = Email.From(email);

            // Assert
            result.Value.Should().Be(email);
        }

        [Theory]
        [InlineData("test@example")] // Missing top-level domain
        [InlineData("test.example.com")] // Missing @ symbol
        [InlineData("test@example@com")] // Multiple @ symbols
        [InlineData("test@example..com")] // Double period in domain
        public void From_InvalidEmail_ThrowsException(string email)
        {
            // Act & Assert
            FluentActions.Invoking(() => Email.From(email)).Should().Throw<CustomException>();
        }

        [Fact]
        public void Equals_TwoEqualEmails_ReturnsTrue()
        {
            // Arrange
            var email1 = Email.From("test@example.com");
            var email2 = Email.From("test@example.com");

            // Act
            var result = email1.Equals(email2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_TwoDifferentEmails_ReturnsFalse()
        {
            // Arrange
            var email1 = Email.From("test@example.com");
            var email2 = Email.From("another.email@example.co.uk");

            // Act
            var result = email1.Equals(email2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

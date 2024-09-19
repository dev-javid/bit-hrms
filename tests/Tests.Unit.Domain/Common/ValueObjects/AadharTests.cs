namespace Tests.Unit.Domain.Common.ValueObjects
{
    public class AadharTests
    {
        [Theory]
        [InlineData("123456789012")]
        [InlineData("987654321098")]
        public void From_ValidAadhar_ReturnsAadhar(string aadhar)
        {
            // Act
            var result = Aadhar.From(aadhar);

            // Assert
            result.Value.Should().Be(aadhar);
        }

        [Theory]
        [InlineData("12345678")] // Incorrect number of digits
        [InlineData("12345678901")] // Incorrect number of digits
        [InlineData("12AB56789012")] // Contains non-numeric characters
        [InlineData("123456AB9012")] // Contains non-numeric characters
        [InlineData("12345678ABCD")] // Contains non-numeric characters
        public void From_InvalidAadhar_ThrowsException(string aadhar)
        {
            // Act & Assert
            FluentActions.Invoking(() => Aadhar.From(aadhar)).Should().Throw<CustomException>();
        }

        [Fact]
        public void Equals_TwoEqualAadhars_ReturnsTrue()
        {
            // Arrange
            var aadhar1 = Aadhar.From("123456789012");
            var aadhar2 = Aadhar.From("123456789012");

            // Act
            var result = aadhar1.Equals(aadhar2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_TwoDifferentAadhars_ReturnsFalse()
        {
            // Arrange
            var aadhar1 = Aadhar.From("123456789012");
            var aadhar2 = Aadhar.From("987654321098");

            // Act
            var result = aadhar1.Equals(aadhar2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

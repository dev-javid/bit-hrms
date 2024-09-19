namespace Tests.Unit.Domain.Common.ValueObjects
{
    public class PANTests
    {
        [Theory]
        [InlineData("ABCDE1234F")]
        [InlineData("WXYZA5678P")]
        [InlineData("WXYZA5678G")]
        public void From_ValidPAN_ReturnsPAN(string pan)
        {
            // Act
            var result = PAN.From(pan);

            // Assert
            result.Value.Should().Be(pan);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("ABCD")]
        [InlineData("WXYZA5678PX")]
        [InlineData("WXYZA5678")]
        [InlineData("WXYZA5678PR")]
        public void From_InvalidPAN_ThrowsException(string pan)
        {
            // Act & Assert
            FluentActions.Invoking(() => PAN.From(pan)).Should().Throw<CustomException>();
        }

        [Fact]
        public void Equals_TwoEqualPANs_ReturnsTrue()
        {
            // Arrange
            var pan1 = PAN.From("ABCDE1234F");
            var pan2 = PAN.From("ABCDE1234F");

            // Act
            var result = pan1.Equals(pan2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_TwoDifferentPANs_ReturnsFalse()
        {
            // Arrange
            var pan1 = PAN.From("ABCDE1234F");
            var pan2 = PAN.From("WXYZA5678P");

            // Act
            var result = pan1.Equals(pan2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

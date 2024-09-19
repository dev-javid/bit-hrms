namespace Tests.Unit.Domain.Common.ValueObjects
{
    public class FileNameTests
    {
        [Theory]
        [InlineData("example.txt")]
        [InlineData("document.pdf")]
        public void From_ValidFileName_ReturnsFileName(string fileName)
        {
            // Act
            var result = FileName.From(fileName);

            // Assert
            result.Value.Should().Be(fileName);
        }

        [Theory]
        [InlineData(" ")] // Null value
        [InlineData("")] // Empty value
        public void From_InvalidFileName_ThrowsException(string fileName)
        {
            // Act & Assert
            FluentActions.Invoking(() => FileName.From(fileName)).Should().Throw<CustomException>();
        }

        [Theory]
        [InlineData("example")] // No extension
        [InlineData("example.pdf.xml")] // Multiple extensions
        public void From_InvalidFileNameWithInvalidExtension_ThrowsException(string fileName)
        {
            // Act & Assert
            FluentActions.Invoking(() => FileName.From(fileName)).Should().Throw<CustomException>();
        }

        [Fact]
        public void Equals_TwoEqualFileNames_ReturnsTrue()
        {
            // Arrange
            var fileName1 = FileName.From("example.txt");
            var fileName2 = FileName.From("example.txt");

            // Act
            var result = fileName1.Equals(fileName2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_TwoDifferentFileNames_ReturnsFalse()
        {
            // Arrange
            var fileName1 = FileName.From("example.txt");
            var fileName2 = FileName.From("document.pdf");

            // Act
            var result = fileName1.Equals(fileName2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

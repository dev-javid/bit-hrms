namespace Tests.Unit.Domain.Common.ValueObjects
{
    public class FinancialMonthTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void From_ValidMonth_ReturnsFinancialMonth(int month)
        {
            // Act
            var financialMonth = FinancialMonth.From(month);

            // Assert
            financialMonth.Value.Should().Be(month);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public void From_InvalidMonth_ThrowsException(int month)
        {
            // Act & Assert
            FluentActions.Invoking(() => FinancialMonth.From(month)).Should().Throw<CustomException>();
        }

        [Fact]
        public void Equals_TwoEqualFinancialMonths_ReturnsTrue()
        {
            // Arrange
            var financialMonth1 = FinancialMonth.From(3);
            var financialMonth2 = FinancialMonth.From(3);

            // Act
            var result = financialMonth1.Equals(financialMonth2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_TwoDifferentFinancialMonths_ReturnsFalse()
        {
            // Arrange
            var financialMonth1 = FinancialMonth.From(1);
            var financialMonth2 = FinancialMonth.From(3);

            // Act
            var result = financialMonth1.Equals(financialMonth2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

namespace Tests.Unit.Domain.Common
{
    public class ExtensionTests
    {
        [Theory]
        [InlineData(7, 28)]
        [InlineData(7, 27)]
        [InlineData(7, 21)]
        [InlineData(7, 14)]
        [InlineData(7, 13)]
        [InlineData(7, 7)]
        [InlineData(6, 29)]
        [InlineData(6, 30)]
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        [InlineData(5, 26)]
        public void IsWeeklyOff_Should_Return_True(int month, int day)
        {
            // Arrange
            var date = new DateTime(2024, month, day, 0, 0, 0, 0, 0, DateTimeKind.Unspecified);

            // Act
            var result = date.IsWeeklyOff();

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(7, 29)]
        [InlineData(7, 26)]
        [InlineData(7, 20)]
        [InlineData(7, 12)]
        [InlineData(7, 11)]
        [InlineData(7, 10)]
        [InlineData(7, 9)]
        [InlineData(7, 6)]
        [InlineData(6, 3)]
        [InlineData(5, 31)]
        public void IsWeeklyOff_Should_Return_False(int month, int day)
        {
            // Arrange
            var date = new DateTime(2024, month, day, 0, 0, 0, 0, 0, DateTimeKind.Unspecified);

            // Act
            var result = date.IsWeeklyOff();

            // Assert
            result.Should().BeFalse();
        }
    }
}

using Simulator;

namespace TestSimulator;

public class PointTests
{
    // Test dla konstrukcji punktu
    [Theory]
    [InlineData(0, 0)]
    [InlineData(5, 10)]
    [InlineData(-5, -10)]
    [InlineData(100, -100)]
    public void Constructor_ShouldSetCorrectValues(int x, int y)
    {
        // Arrange & Act
        var point = new Point(x, y);

        // Assert
        Assert.Equal(x, point.X);
        Assert.Equal(y, point.Y);
    }

    // Test dla metody ToString()
    [Theory]
    [InlineData(0, 0, "(0, 0)")]
    [InlineData(5, 10, "(5, 10)")]
    [InlineData(-5, -10, "(-5, -10)")]
    [InlineData(100, -100, "(100, -100)")]
    public void ToString_ShouldReturnCorrectString(int x, int y, string expectedString)
    {
        // Arrange
        var point = new Point(x, y);

        // Act
        var result = point.ToString();

        // Assert
        Assert.Equal(expectedString, result);
    }

    // Testy dla metody Next
    [Theory]
    // Ruch w prawo
    [InlineData(0, 0, Direction.Right, 1, 0)]
    [InlineData(5, 5, Direction.Right, 6, 5)]
    [InlineData(-5, -5, Direction.Right, -4, -5)]

    // Ruch w lewo
    [InlineData(0, 0, Direction.Left, -1, 0)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    [InlineData(-5, -5, Direction.Left, -6, -5)]

    // Ruch w dół
    [InlineData(0, 0, Direction.Down, 0, -1)]
    [InlineData(5, 5, Direction.Down, 5, 4)]
    [InlineData(-5, -5, Direction.Down, -5, -6)]

    // Ruch w górę
    [InlineData(0, 0, Direction.Up, 0, 1)]
    [InlineData(5, 5, Direction.Up, 5, 6)]
    [InlineData(-5, -5, Direction.Up, -5, -4)]

    public void Next_ShouldMoveCorrectly(int startX, int startY, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var point = new Point(startX, startY);

        // Act
        var newPoint = point.Next(direction);

        // Assert
        Assert.Equal(new Point(expectedX, expectedY), newPoint);
    }

    // Testy dla metody NextDiagonal
    [Theory]
    // Obrócone ruchy o 45 stopni zgodnie z ruchem wskazówek zegara
    [InlineData(0, 0, Direction.Right, 1, -1)]  // Right -> UpRight (45° clockwise)
    [InlineData(5, 5, Direction.Right, 6, 4)]
    [InlineData(-5, -5, Direction.Right, -4, -6)]

    [InlineData(0, 0, Direction.Left, -1, 1)]  // Left -> DownLeft (45° clockwise)
    [InlineData(5, 5, Direction.Left, 4, 6)]
    [InlineData(-5, -5, Direction.Left, -6, -4)]

    [InlineData(0, 0, Direction.Down, -1, -1)]  // Down -> BottomLeft (45° clockwise)
    [InlineData(5, 5, Direction.Down, 4, 4)]
    [InlineData(-5, -5, Direction.Down, -6, -6)]

    [InlineData(0, 0, Direction.Up, 1, 1)]  // Up -> TopRight (45° clockwise)
    [InlineData(5, 5, Direction.Up, 6, 6)]
    [InlineData(-5, -5, Direction.Up, -4, -4)]

    public void NextDiagonal_ShouldMoveCorrectly(int startX, int startY, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var point = new Point(startX, startY);

        // Act
        var newPoint = point.NextDiagonal(direction);

        // Assert
        Assert.Equal(new Point(expectedX, expectedY), newPoint);
    }
}
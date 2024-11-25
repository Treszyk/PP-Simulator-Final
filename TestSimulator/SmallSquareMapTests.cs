using Simulator.Maps;
using Simulator;

namespace TestSimulator;

public class SmallSquareMapTests
{
    // Test dla konstruktorów mapy - poprawność ustawienia rozmiaru mapy
    [Fact]
    public void Constructor_ValidSize_ShouldSetSize()
    {
        // Arrange
        int size = 10;

        // Act
        var map = new SmallSquareMap(size, size);

        // Assert
        Assert.Equal(size, map.SizeX);
        Assert.Equal(size, map.SizeY);
    }

    // Test dla konstruktorów mapy - niepoprawne rozmiary mapy
    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException(int size)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SmallSquareMap(size, size));
    }

    // Testy dla metody Exist
    [Theory]
    [InlineData(3, 4, 5, true)]    // Punkt (3, 4) na mapie 5x5
    [InlineData(6, 1, 5, false)]   // Punkt (6, 1) poza mapą 5x5
    [InlineData(19, 19, 20, true)] // Punkt (19, 19) na mapie 20x20
    [InlineData(20, 20, 20, false)] // Punkt (20, 20) poza mapą 20x20
    public void Exist_ShouldReturnCorrectValue(int x, int y, int size, bool expected)
    {
        // Arrange
        var map = new SmallSquareMap(size, size);
        var point = new Point(x, y);

        // Act
        var result = map.Exist(point);

        // Assert
        Assert.Equal(expected, result);
    }

    // Testy dla metody Next
    [Theory]
    [InlineData(5, 10, Direction.Up, 5, 11)]   // Ruch w górę: punkt nie wychodzi poza mapę
    [InlineData(0, 0, Direction.Down, 0, 0)]   // Ruch w dół: punkt wyszedłby poza mapę
    [InlineData(0, 8, Direction.Left, 0, 8)]   // Ruch w lewo: punkt wyszedłby poza mapę
    [InlineData(19, 10, Direction.Right, 19, 10)] // Ruch w prawo: punkt nie wychodzi poza mapę
    public void Next_ShouldReturnCorrectNextPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallSquareMap(20, 20); // Zmieniamy na mapę 20x20
        var point = new Point(x, y);

        // Act
        var nextPoint = map.Next(point, direction);

        // Assert
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }

    // Testy dla metody NextDiagonal
    [Theory]
    [InlineData(5, 10, Direction.Up, 6, 11)]     // Ruch w górę-diagonalnie: punkt nie wychodzi poza mapę
    [InlineData(0, 0, Direction.Down, 0, 0)]     // Ruch w dół-diagonalnie: punkt wyszedłby poza mapę
    [InlineData(0, 8, Direction.Left, 0, 8)]     // Ruch w lewo-diagonalnie: punkt wyszedłby poza mapę
    [InlineData(19, 10, Direction.Right, 19, 10)] // Ruch w prawo-diagonalnie: punkt wyszedłby poza mapę
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallSquareMap(20, 20); // Zmieniamy na mapę 20x20
        var point = new Point(x, y);

        // Act
        var nextPoint = map.NextDiagonal(point, direction);

        // Assert
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
}

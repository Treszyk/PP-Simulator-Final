using Simulator;
namespace TestSimulator;

public class RectangleTests
{
    // Testowanie konstrukcji prostokąta z poprawnymi współrzędnymi
    [Fact]
    public void Constructor_ValidCoordinates_ShouldSetProperties()
    {
        // Arrange
        int x1 = 2, y1 = 3, x2 = 5, y2 = 7;

        // Act
        var rectangle = new Rectangle(x1, y1, x2, y2);

        // Assert
        Assert.Equal(2, rectangle.X1);
        Assert.Equal(3, rectangle.Y1);
        Assert.Equal(5, rectangle.X2);
        Assert.Equal(7, rectangle.Y2);
    }

    // Testowanie błędu w przypadku "chudego" prostokąta
    [Theory]
    [InlineData(0, 0, 0, 5)] // Chudy prostokąt (X1 == X2)
    [InlineData(0, 0, 5, 0)] // Chudy prostokąt (Y1 == Y2)
    public void Constructor_InvalidCoordinates_ShouldThrowArgumentException(int x1, int y1, int x2, int y2)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Rectangle(x1, y1, x2, y2));
    }

    // Testowanie zamiany współrzędnych, kiedy X1, Y1 są większe niż X2, Y2
    [Theory]
    [InlineData(5, 7, 2, 3, 2, 3, 5, 7)] // Przypadek X1 > X2, Y1 > Y2
    [InlineData(10, 12, 3, 8, 3, 8, 10, 12)] // Kolejny przypadek X1 > X2, Y1 > Y2
    public void Constructor_ReversedCoordinates_ShouldSwapCoordinates(int x1, int y1, int x2, int y2,
                                                                     int expectedX1, int expectedY1,
                                                                     int expectedX2, int expectedY2)
    {
        // Act
        var rectangle = new Rectangle(x1, y1, x2, y2);

        // Assert
        Assert.Equal(expectedX1, rectangle.X1);
        Assert.Equal(expectedY1, rectangle.Y1);
        Assert.Equal(expectedX2, rectangle.X2);
        Assert.Equal(expectedY2, rectangle.Y2);
    }

    // Testowanie dodatkowych przypadków konstrukcji prostokąta z różnymi współrzędnymi
    [Theory]
    [InlineData(5, 7, 2, 3, 2, 3, 5, 7)] // X1 > X2, Y1 > Y2
    [InlineData(2, 3, 5, 7, 2, 3, 5, 7)] // X1 < X2, Y1 < Y2 (przypadek poprawny)
    public void Constructor_VariedCoordinates_ShouldHandleAllCases(int x1, int y1, int x2, int y2,
                                                                 int expectedX1, int expectedY1,
                                                                 int expectedX2, int expectedY2)
    {
        // Act
        var rectangle = new Rectangle(x1, y1, x2, y2);

        // Assert
        Assert.Equal(expectedX1, rectangle.X1);
        Assert.Equal(expectedY1, rectangle.Y1);
        Assert.Equal(expectedX2, rectangle.X2);
        Assert.Equal(expectedY2, rectangle.Y2);
    }

    // Testowanie metody Contains
    [Theory]
    [InlineData(5, 5, true)]  // Punkt wewnątrz prostokąta
    [InlineData(0, 0, true)] // Punkt w lewym dolnym rogu
    [InlineData(10, 10, true)]  // Punkt w prawym gornym rogu
    [InlineData(10, 11, false)] // Punkt poza prostokątem
    [InlineData(-1, 10, false)] // Punkt poza prostokątem
    public void Contains_ShouldReturnCorrectResult(int px, int py, bool expected)
    {
        // Arrange
        var rectangle = new Rectangle(0, 0, 10, 10);
        var point = new Point(px, py);

        // Act
        var result = rectangle.Contains(point);

        // Assert
        Assert.Equal(expected, result);
    }

    // Testowanie metody ToString
    [Theory]
    [InlineData(2, 3, 5, 7, "(2, 3):(5, 7)")]  // Punkt wewnątrz prostokąta
    [InlineData(5, 7, 2, 3, "(2, 3):(5, 7)")] // Punkt w lewym dolnym rogu
    public void ToString_ShouldReturnCorrectString(int x1, int y1, int x2, int y2, string expected)
    {
        // Arrange
        var rectangle = new Rectangle(2, 3, 5, 7);

        // Act
        var result = rectangle.ToString();

        // Assert
        Assert.Equal(expected, result);
    }

    // Testowanie metody ToString dla odwrotnych współrzędnych
    [Fact]
    public void Constructor_ReverseCoordinates_ShouldStillSetCorrectCoordinates()
    {
        // Arrange
        var rectangle = new Rectangle(5, 7, 2, 3);

        // Act
        var result = rectangle.ToString();

        // Assert
        Assert.Equal("(2, 3):(5, 7)", result);
    }
}

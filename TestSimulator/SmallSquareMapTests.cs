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

    // Testy dla dodawania stworzeń na mapie
    [Theory]
    [InlineData(5, 9)]
    [InlineData(0, 0)]
    [InlineData(9, 9)]
    public void AddCreature_ShouldAddToCorrectPosition(int x, int y)
    {
        // Arrange
        var map = new SmallSquareMap(10, 10);
        var creature = new Orc("TestCreature", 1);
        var position = new Point(x, y);

        // Act
        map.Add(creature, position);

        // Assert
        Assert.Contains(creature, map.At(position));
        Assert.Equal(position, creature.Position);
    }
    [Theory]
    [InlineData(19, 10)]
    [InlineData(0, -1)]
    [InlineData(10, 5)]
    public void AddCreature_ShouldThrowArgumentExceptionIfInvalidPosition(int x, int y)
    {
        // Arrange
        var map = new SmallSquareMap(10, 10);
        var creature = new Orc("TestCreature", 1);
        var position = new Point(x, y);

        // Act and Assert
        Assert.Throws<ArgumentException>(() => map.Add(creature, position));
    }
    // Testy dla usuwania stworzeń z mapy
    [Theory]
    [InlineData(5, 9)]
    [InlineData(0, 0)]
    [InlineData(9, 9)]
    public void RemoveCreature_ShouldRemoveFromMap(int x, int y)
    {
        // Arrange
        var map = new SmallSquareMap(10, 10);
        var creature = new Orc("TestCreature", 1);
        var position = new Point(x, y);

        map.Add(creature, position);

        // Act
        map.Remove(creature);

        // Assert
        Assert.DoesNotContain(creature, map.At(position));
    }

    // Testy dla przemieszczania stworzeń
    [Theory]
    [InlineData(5, 9, 8, 9)]
    [InlineData(0, 0, 5, 5)]
    [InlineData(9, 9, 7, 3)]
    public void MoveCreature_ShouldChangePositionOnMap(int fromX, int fromY, int toX, int toY)
    {
        // Arrange
        var map = new SmallSquareMap(10, 10);
        var creature = new Orc("TestCreature", 1);
        var from = new Point(fromX, fromY);
        var to = new Point(toX, toY);

        map.Add(creature, from);

        // Act
        map.Move(creature, from, to);

        // Assert
        Assert.DoesNotContain(creature, map.At(from));
        Assert.Contains(creature, map.At(to));
        Assert.Equal(to, creature.Position);
    }

    // Testy dla ruchu stworzenia za pomocą metody Go 
    [Theory]
    [InlineData(5, 8)]
    [InlineData(1, 0)]
    [InlineData(9, 8)]
    public void CreatureGo_ShouldUpdatePositionCorrectly(int x, int y)
    {
        // Arrange
        var map = new SmallSquareMap(10, 10);
        var creature = new Orc("TestCreature", 1);
        var position = new Point(x, y);

        creature.AssignMap(map, position);

        // Act
        creature.Go(Direction.Up);
        creature.Go(Direction.Left);

        // Assert - oczekiwany ruch do góry i potem w lewo
        Assert.Equal(new Point(x - 1, y + 1), creature.Position);
        Assert.Contains(creature, map.At(new Point(x-1, y+1)));
        Assert.DoesNotContain(creature, map.At(position));
    }
}

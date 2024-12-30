namespace Simulator.Utilities;
public readonly struct Point
{
    public readonly int X, Y;
    public Point(int x, int y) => (X, Y) = (x, y);
    public override string ToString() => $"({X}, {Y})";

    public Point Next(Direction direction, int AddiMoves = 0)
    {
        int new_x = X;
        int new_y = Y;
        switch (direction)
        {
            case Direction.Right:
                new_x++; break;
            case Direction.Left:
                new_x--; break;
            case Direction.Down:
                new_y--; break;
            case Direction.Up:
                new_y++; break;
        }
        return new Point(new_x, new_y);
    }

    // rotate given direction 45 degrees clockwise
    public Point NextDiagonal(Direction direction)
    {
        int new_x = X;
        int new_y = Y;
        switch (direction)
        {
            case Direction.Right:
                new_x++; new_y--; break;
            case Direction.Left:
                new_x--; new_y++; break;
            case Direction.Down:
                new_x--; new_y--; break;
            case Direction.Up:
                new_x++; new_y++; break;
        }
        return new Point(new_x, new_y);
    }

    public static bool operator ==(Point p1, Point p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator !=(Point p1, Point p2)
    {
        return !p1.Equals(p2);
    }
}

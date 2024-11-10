namespace Simulator;

public class Rectangle
{
    public int X1 { get; }
    public int Y1 { get; }
    public int X2 { get; }
    public int Y2 { get; }

    public Rectangle(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2 || y1 == y2)
            throw new ArgumentException("nie chcemy \"chudych\" prostokątów");

        //ustawianie wspolrzednych lewego dolnego rogu
        X1 = Math.Min(x1, x2);
        Y1 = Math.Min(y1, y2);

        //ustawianie wspolrzednych prawego gornego rogu
        X2 = Math.Max(x1, x2);
        Y2 = Math.Max(y1, y2);
    }

    public Rectangle(Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y) { }

    public bool Contains(Point point)
    {
        return point.X >= X1 && point.X <= X2 && point.Y >= Y1 && point.Y <= Y2;
    }

    public override string ToString()
    {
        return $"({X1}, {Y1}):({X2}, {Y2})";
    }
}

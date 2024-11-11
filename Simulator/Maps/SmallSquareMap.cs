namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }
    private Rectangle _mapRect;

    public SmallSquareMap(int size)
    {
        if(size < 5 || size > 20)
            throw new ArgumentOutOfRangeException($"Podany rozmiar nie mieści się w przedziale od 5 do 20. Podany rozmiar: {size}");
        Size = size;
        _mapRect = new(new Point(0, 0), new Point(Size - 1, Size - 1));
    }
    public override bool Exist(Point p)
    {
        return _mapRect.Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);
        return Exist(next) ? next : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextDiag = p.NextDiagonal(d);
        return Exist(nextDiag) ? nextDiag : p;
    }
}

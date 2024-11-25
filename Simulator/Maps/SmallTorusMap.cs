using static System.Net.Mime.MediaTypeNames;

namespace Simulator.Maps;

public class SmallTorusMap : SmallMap
{
    public int Size { get; }
    public SmallTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }
    public override bool Exist(Point p)
    {
        return _mapRect.Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);
        return Exist(next) ? next : new Point((next.X + SizeX) % SizeX, (next.Y + SizeY) % SizeY);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextDiag = p.NextDiagonal(d);
        return Exist(nextDiag) ? nextDiag : new Point((nextDiag.X + SizeX) % SizeX, (nextDiag.Y + SizeY) % SizeY);
    }
}

namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public int Size { get; }
    public SmallSquareMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }
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

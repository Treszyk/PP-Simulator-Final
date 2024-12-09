namespace Simulator.Maps;

public class BigBounceMap : BigMap
{
    public BigBounceMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }

    public override bool Exist(Point p)
    {
        return _mapRect.Contains(p);
    }
    public static Direction BouncedDirection(Direction d) => d switch
    {
        Direction.Up => Direction.Down,
        Direction.Down => Direction.Up,
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        _ => d,
    };
    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);
        //Console.WriteLine($"{Exist(next)} {next}");
        return Exist(next) ? next : p.Next(BouncedDirection(d));
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        //z racji ze minimalne rozmiary mapy to 5x5 to takie sprawdzanie odbic powinno byc wystarczajace
        Point nextDiag = p.NextDiagonal(d);
        if (Exist(nextDiag))
        {
            return nextDiag;
        }
        Point bouncedDiag = p.NextDiagonal(BouncedDirection(d));
        //Console.WriteLine($"bounceddiag: {bouncedDiag}");
        return Exist(bouncedDiag) ? bouncedDiag : p;
    }
}

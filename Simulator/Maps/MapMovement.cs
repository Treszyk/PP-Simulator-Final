namespace Simulator.Maps;

internal static class MapMovement
{
    public static Point WallNext(Map map, Point p, Direction d)
    {
        Point next = p.Next(d);
        return map.Exist(next) ? next : p;
    }
    public static Point WallNextDiagonal(Map map, Point p, Direction d)
    {
        Point nextDiag = p.NextDiagonal(d);
        return map.Exist(nextDiag) ? nextDiag : p;
    }
    public static Point TorusNext(Map map, Point p, Direction d)
    {
        Point next = p.Next(d);
        return map.Exist(next) ? next : new Point((next.X + map.SizeX) % map.SizeX, (next.Y + map.SizeY) % map.SizeY);
    }
    public static Point TorusNextDiagonal(Map map, Point p, Direction d)
    {
        Point nextDiag = p.NextDiagonal(d);
        return map.Exist(nextDiag) ? nextDiag : new Point((nextDiag.X + map.SizeX) % map.SizeX, (nextDiag.Y + map.SizeY) % map.SizeY);
    }
    public static Direction BouncedDirection(Direction d) => d switch
    {
        Direction.Up => Direction.Down,
        Direction.Down => Direction.Up,
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        _ => d,
    };
    public static Point BounceNext(Map map, Point p, Direction d)
    {
        Point next = p.Next(d);
        return map.Exist(next) ? next : p.Next(BouncedDirection(d));
    }
    public static Point BounceNextDiagonal(Map map, Point p, Direction d)
    {
        //z racji ze minimalne rozmiary mapy to 5x5 to takie sprawdzanie odbic powinno byc wystarczajace
        Point nextDiag = p.NextDiagonal(d);
        if (map.Exist(nextDiag))
        {
            return nextDiag;
        }
        Point bouncedDiag = p.NextDiagonal(BouncedDirection(d));
        //Console.WriteLine($"bounceddiag: {bouncedDiag}");
        return map.Exist(bouncedDiag) ? bouncedDiag : p;
    }

}

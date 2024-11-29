using System.Drawing;

namespace Simulator.Maps;

public abstract class Map
{
    public int SizeX { get; }
    public int SizeY { get; }
    protected Rectangle _mapRect;
    public Map(int sizeX, int sizeY)
    {
        if (sizeX < 5 || sizeY < 5)
            throw new ArgumentOutOfRangeException($"Wymiary mapy muszą być co najmniej 5x5. Twoje wymiary: {sizeX}x{sizeY}");
        SizeX = sizeX;
        SizeY = sizeY;
        _mapRect = new(new Point(0, 0), new Point(SizeX - 1, SizeY - 1));
    }
    public abstract bool Exist(Point p);
    public abstract Point Next(Point p, Direction d);
    public abstract Point NextDiagonal(Point p, Direction d);
    public abstract void Add(Point position, IMappable mappable);
    public abstract void Remove(Point point, IMappable mappable);
    public abstract void Move(IMappable mappable, Point from, Point to, Direction direction);
    public abstract List<IMappable> At(Point position);
    public abstract List<IMappable> At(int x, int y);
}

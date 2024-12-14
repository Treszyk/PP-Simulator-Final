using System.Drawing;

namespace Simulator.Maps;

public abstract class Map
{
    public int SizeX { get; }
    public int SizeY { get; }
    protected Rectangle _mapRect;
    public readonly Dictionary<Point, List<IMappable>> MappablePositions;
    public Map(int sizeX, int sizeY)
    {
        if (sizeX < 5 || sizeY < 5)
            throw new ArgumentOutOfRangeException($"Wymiary mapy muszą być co najmniej 5x5. Twoje wymiary: {sizeX}x{sizeY}");
        SizeX = sizeX;
        SizeY = sizeY;
        _mapRect = new(new Point(0, 0), new Point(SizeX - 1, SizeY - 1));
        MappablePositions = [];
    }
    public abstract bool Exist(Point p);
    public abstract Point Next(Point p, Direction d);
    public abstract Point NextDiagonal(Point p, Direction d);
    public void Add(Point position, IMappable mappable)
    {
        if (!Exist(position))
            throw new ArgumentException($"Pozycja spoza zakresu mapy {position}");
        if (!MappablePositions.ContainsKey(position))
            MappablePositions[position] = [];
        MappablePositions[position].Add(mappable);
    }
    public void Remove(Point point, IMappable mappable)
    {
        if (!MappablePositions.TryGetValue(point, out List<IMappable>? value))
            return;
        value.Remove(mappable);
        if (value.Count == 0)
            MappablePositions.Remove(point);
    }
    public void Move(IMappable mappable, Point from, Point to, Direction direction)
    {
        if (!Exist(to))
            throw new ArgumentException($"Docelowa pozycja spoza zakresu mapy {to}");

        if (!MappablePositions.TryGetValue(from, out List<IMappable>? value))
            return;
        if (value.Remove(mappable))
        {
            Add(to, mappable);
        }
    }
    public List<IMappable> At(Point position)
    {
        return MappablePositions.TryGetValue(position, out var mappables) ? mappables : ([]);
    }
    public List<IMappable> At(int x, int y) => At(new Point(x, y));
}

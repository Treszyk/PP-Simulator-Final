using System.Drawing;
using System.Runtime.CompilerServices;
using Simulator.Utilities;
using Point = Simulator.Utilities.Point;
using Rectangle = Simulator.Utilities.Rectangle;

namespace Simulator.Maps;
public abstract class Map
{
    public int SizeX { get; }
    public int SizeY { get; }
    public Func<Map, Point, Direction, Point>? FNext, FNextDiagonal;
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
    public bool Exist(Point p)
    {
        return _mapRect.Contains(p);
    }
    public Point Next(Point p, Direction d) => FNext?.Invoke(this, p, d) ?? p;
    public Point NextDiagonal(Point p, Direction d) => FNextDiagonal?.Invoke(this, p, d) ?? p;
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
            Remove(from, mappable);
            Add(to, mappable);
        }
    }
    public List<IMappable> At(Point position)
    {
        return MappablePositions.TryGetValue(position, out var mappables) ? mappables : ([]);
    }
    public List<IMappable> At(int x, int y) => At(new Point(x, y));
    public float GetDistance(Point p1, Point p2)
    {
        float deltaX = p2.X - p1.X;
        float deltaY = p2.Y - p1.Y;

        return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
}

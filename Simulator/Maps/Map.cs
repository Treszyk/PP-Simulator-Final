using System.Drawing;

namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
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
    /// <summary>
    /// Check if give point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public abstract bool Exist(Point p);

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction 
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);

    public abstract void Add(Point position, IMappable mappable);
    public abstract void Remove(Point point, IMappable mappable);
    public abstract void Move(IMappable mappable, Point from, Point to, Direction direction);
    public abstract List<IMappable> At(Point position);
    public abstract List<IMappable> At(int x, int y);
}

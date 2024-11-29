namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private readonly Dictionary<Point, List<IMappable>> _mappablePositions;
    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20 || sizeY > 20)
            throw new ArgumentOutOfRangeException("Wymiary mapy nie mogą przekraczać 20x20. Twoje wymiary: {sizeX}x{sizeY}");
        _mappablePositions = new Dictionary<Point, List<IMappable>>();
    }

    public override void Add(Point position, IMappable mappable)
    {
        if (!Exist(position))
            throw new ArgumentException($"Pozycja spoza zakresu mapy {position}");

        if (!_mappablePositions.ContainsKey(position))
            _mappablePositions[position] = [];

        _mappablePositions[position].Add(mappable);
    }

    public override void Remove(Point point, IMappable mappable)
    {
        if (!_mappablePositions.ContainsKey(point))
            return;

        _mappablePositions[point].Remove(mappable);

        if (_mappablePositions[point].Count == 0)
            _mappablePositions.Remove(point);
    }
    public override void Move(IMappable mappable, Point from, Point to, Direction direction)
    {
        if (!Exist(to))
            throw new ArgumentException($"Docelowa pozycja spoza zakresu mapy {to}");

        if (!_mappablePositions.ContainsKey(from))
            return;
        if (_mappablePositions[from].Remove(mappable))
        {
            Add(to, mappable);
        }
            
    }
    public override List<IMappable> At(Point position)
    {
        if (_mappablePositions.TryGetValue(position, out var mappables))
            return mappables;

        return [];
    }
    public override List<IMappable> At(int x, int y) => At(new Point(x, y));

}

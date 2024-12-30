using Simulator.Maps;
using Simulator.Utilities;
using Point = Simulator.Utilities.Point;
namespace Simulator.Entities;

public class Animals : IMappable
{
    private string _description = "Unknown";
    public virtual char Symbol => char.ToUpper(GetType().Name[0]);
    public required string Description
    {
        get { return _description; }
        init => _description = Validator.Shortener(value, 3, 15, '#');
    }
    public uint Size { get; set; } = 3;
    public bool IsDead => Health <= 0;
    public virtual string Info => $"{Description} <{Size}>";
    public bool IsInBattle { get; set; }
    public Point Position { get; protected set; }
    public Map? Map { get; private set; }
    public Direction LastMove { get; set; }
    public IMappable? Target { get; set; }
    public int Health { get; set; } = 10;

    public virtual void Go()
    {
        if (Map == null)
            throw new InvalidOperationException("Stwór nie jest przypisany do mapy.");

        Random rand = new Random();
        Direction direction = (Direction)rand.Next(4);
        LastMove = direction;
        Point newPosition = Map.Next(Position, direction);
        Map.Move(this, Position, newPosition, direction);
        Position = newPosition;
    }
    public void InitMapAndPosition(Map map, Point position)
    {
        if (!map.Exist(position))
            throw new ArgumentException("Pozycja spoza zakresu mapy.", nameof(position));
        if (Map != null)
            throw new InvalidOperationException($"Zwierze {Description} jest już przypisany do mapy i nie może zostać przeniesiony na inną mapę.");
        Map = map;
        Position = position;
    }
    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";
}

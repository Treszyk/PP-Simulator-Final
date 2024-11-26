using Simulator.Maps;

namespace Simulator;

public abstract class Creature
{
    private string _name = "Unknown";
    private int _level = 1;

    public string Name
    {
        get => _name;
        init => _name = Validator.Shortener(value, 3, 25, '#');
    }

    public int Level
    {
        get => _level;
        init => _level = Validator.Limiter(value, 1, 10);
    }

    public abstract int Power { get; }
    public abstract string Info { get; }

    public Point Position { get; internal set; }
    public Map? Map { get; private set; }

    public Creature() { }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public void AssignMap(Map map, Point position)
    {
        if (!map.Exist(position))
            throw new ArgumentException("Pozycja spoza zakresu mapy.", nameof(position));

        if (Map != null)
            throw new InvalidOperationException($"Stwór {Name} jest już przypisany do mapy i nie może zostać przeniesiony na inną mapę.");

        Map = map;
        Position = position;
        Map.Add(this, position);
        //Console.WriteLine("map assigned");
    }
    public abstract string Greeting();
    public void Upgrade() => _level = _level < 10 ? _level + 1 : _level;
    public void Go(Direction direction)
    {
        if (Map == null)
            throw new InvalidOperationException("Stwór nie jest przypisany do mapy.");

        Point newPosition = Map.Next(Position, direction);
        Map.Move(this, Position, newPosition);
        Position = newPosition;
        //Console.WriteLine("Creature moved");
    }

    public override string ToString() => $"{this.GetType().Name.ToUpper()}: {Info}";
}

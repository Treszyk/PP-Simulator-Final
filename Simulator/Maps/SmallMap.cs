namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private readonly Dictionary<Point, List<Creature>> _creaturePositions;
    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20 || sizeY > 20)
            throw new ArgumentOutOfRangeException("Wymiary mapy nie mogą przekraczać 20x20. Twoje wymiary: {sizeX}x{sizeY}");
        _creaturePositions = new Dictionary<Point, List<Creature>>();
    }

    public override void Add(Creature creature, Point position)
    {
        if (!Exist(position))
            throw new ArgumentException($"Pozycja spoza zakresu mapy {position}");

        if (!_creaturePositions.ContainsKey(position))
            _creaturePositions[position] = [];

        _creaturePositions[position].Add(creature);
        creature.Position = position;
        //Console.WriteLine($"Creature added: {_creaturePositions[position]}");
    }

    public override void Remove(Creature creature)
    {
        if (!_creaturePositions.ContainsKey(creature.Position))
            return;

        _creaturePositions[creature.Position].Remove(creature);

        if (_creaturePositions[creature.Position].Count == 0)
            _creaturePositions.Remove(creature.Position);
    }
    public override void Move(Creature creature, Point from, Point to)
    {
        Console.WriteLine($"{Exist(to)} {to}");
        if (!Exist(to))
            throw new ArgumentException($"Docelowa pozycja spoza zakresu mapy {to}");
        //Console.WriteLine(_creaturePositions.ContainsKey(new Point(3, 4)));
        //Console.WriteLine($"Moving from {from}");
        if (!_creaturePositions.ContainsKey(from))
            return;
        //Console.WriteLine(_creaturePositions[from]);
        if (_creaturePositions[from].Remove(creature))
            Add(creature, to);
        //Console.WriteLine(_creaturePositions[from]);
    }
    public override List<Creature> At(Point position)
    {
        if (_creaturePositions.TryGetValue(position, out var creatures))
            return creatures;

        return [];
    }
    public override List<Creature> At(int x, int y) => At(new Point(x, y));

}

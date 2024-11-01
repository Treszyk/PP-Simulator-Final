namespace Simulator;

public abstract class Creature
{
    private string _name = "Unknown";
    private int _level = 1;
    public string Name
    {
        get { return _name; }
        init => _name = Validator.Shortener(value, 3, 25, '#');
    }
    public abstract int Power { get; }
    public int Level
    {
        get { return _level; }
        init => _level = Validator.Limiter(value, 1, 10);
    }
    public abstract string Info { get; }
    public Creature()
    {
        
    }
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }
    public abstract void SayHi();
    public void Upgrade() => _level = _level < 10 ? _level+1 : _level;
    public void Go(Direction direction) => Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}");
    public void Go(Direction[] directions)
    {
        foreach(Direction direction in directions)
            Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}");
    }
    public void Go(string input)
    {
        Go(DirectionParser.Parse(input));
    }
    public override string ToString() => $"{this.GetType().Name.ToUpper()}: {Info}";
}

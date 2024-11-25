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
    public abstract string Greeting();
    public void Upgrade() => _level = _level < 10 ? _level+1 : _level;
    public string Go(Direction direction) => $"{Name} goes {direction.ToString().ToLower()}";
    public string[] Go(List<Direction> directions)
    {
        string[] res = new string[directions.Count];
        int i = 0;
        foreach (Direction direction in directions)
            res[i++] = Go(direction);
        return res;          
    }
    public string[] Go(string input) => Go(DirectionParser.Parse(input));
    public override string ToString() => $"{this.GetType().Name.ToUpper()}: {Info}";
}

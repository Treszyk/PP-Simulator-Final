namespace Simulator;

public class Orc : Creature
{
    private int _rage = 1;
    private int _counter = 0;
    public int Rage
    {
        get => _rage;
        init => _rage = Math.Clamp(value, 0, 10);
    }
    public override int Power => 7 * Level + 3 * Rage;
    public Orc() { }
    public Orc(string name = "Unknown", int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }
    public void Hunt()
    {
        Console.WriteLine($"{Name} is hunting.");
        _counter++;
        if (_counter % 2 == 0 && _rage < 10)
        {
            _rage++;
        }
    }
    public override void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.");
    }
}

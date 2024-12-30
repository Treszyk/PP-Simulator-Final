using Simulator.Utilities;

namespace Simulator.Entities;

public class Orc : Creature
{
    private int _rage = 1;
    private int _counter = 0;
    public int Rage
    {
        get => _rage;
        init => _rage = Validator.Limiter(value, 0, 10);
    }
    public override int Power => 7 * Level + 3 * Rage;
    public override string Info => $"{Name} [{Level}][{Rage}]";
    public Orc() { }
    public Orc(string name = "Unknown", int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }
    public void Hunt()
    {
        _counter++;
        if (_counter % 2 == 0 && _rage < 10)
        {
            _rage++;
        }
    }
    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";
}

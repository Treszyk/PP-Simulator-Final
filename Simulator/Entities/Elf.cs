using Simulator.Utilities;

namespace Simulator.Entities;

public class Elf : Creature
{
    private int _agility = 1;
    private int _counter = 0;
    public int Agility
    {
        get => _agility;
        init => _agility = Validator.Limiter(value, 0, 10);
    }
    public override int Power
    {
        get => 8 * Level + 2 * Agility;
    }
    public override string Info => $"{Name} [{Level}][{Agility}]";
    public Elf() { }
    public Elf(string name = "Unknown", int level = 1, int agility = 1) : base(name, level)
    {
        Agility = agility;
    }
    public void Sing()
    {
        _counter++;
        if (_counter % 3 == 0 && _agility < 10)
        {
            _agility++;
        }
    }
    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
}

namespace Simulator;

public class Elf : Creature
{ 
    private int _agility = 1;
    private int _counter = 0;
    public int Agility
    {
        get => _agility;
        init => _agility = Math.Clamp(value, 0, 10);
    }
    public override int Power
    {
        get => 8 * Level + 2 * Agility;
    }
    public Elf() { }
    public Elf(string name = "Unknown", int level = 1, int agility = 1) : base(name, level)
    {
        Agility = agility;
    }
    public void Sing()
    {
        Console.WriteLine($"{Name} is singing.");
        _counter++;
        if (_counter%3==0 && _agility < 10)
        {
            _agility++;
        }
    }
    public override void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.");
    }
}

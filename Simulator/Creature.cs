namespace Simulator;

public class Creature
{
    string Name { get; set; }
    int Level { get; set; }
    public string Info => $"{Name} [{Level}]";
    public Creature()
    {
        
    }
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }
    public void SayHi() => Console.WriteLine($"Hi, I'm {Name}, my level is {Level}.");
    
}

using Force.DeepCloner;
using Simulator.Maps;
using Simulator.Utilities;
using Action = Simulator.Utilities.Action;

namespace Simulator.Entities;

public abstract class Creature : IMappable
{
    private string _name = "Unknown";
    public Faction Faction { get; init; }
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
    public bool IsDead => Health <= 0;
    public abstract int Power { get; }
    public abstract string Info { get; }
    public Map? Map { get; private set; }
    public Point Position { get; private set; }
    public Point LastPosition { get; set; }
    public Action LastAction { get; set; }
    public Direction LastMove { get; set; }
    public bool IsInBattle { get; set; }
    public IMappable? Target { get; set; }
    public int Health { get; set; } = 20;

    public Creature() { }
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }
    public void InitMapAndPosition(Map map, Point position)
    {
        if (!map.Exist(position))
            throw new ArgumentException("Pozycja spoza zakresu mapy.", nameof(position));
        if (Map != null)
            throw new InvalidOperationException($"Stwór {Name} jest już przypisany do mapy i nie może zostać przeniesiony na inną mapę.");
        Map = map;
        Position = position;
        //Console.WriteLine("map assigned");
    }
    public abstract string Greeting();
    public void Upgrade() => _level = _level < 10 ? _level + 1 : _level;
    public void Go()
    {
        LastPosition = Position;
        if (Map == null)
            throw new InvalidOperationException("Stwór nie jest przypisany do mapy.");
        Random rand = new Random();
        Direction direction;
        Point newPosition;
        if (Target != null && Target.IsDead)
        {
            Target = null;
            IsInBattle = false;
        }

        if(Target == null && Health <= 0.7*15)//base health zamiast 15
        {
            LastAction = Action.Regen;
            Health += (int)(0.2 * 15);//tutaj dac * BaseHealth
        } else if(Target == null)
        {
            direction = (Direction)rand.Next(4);
            newPosition = Map.Next(Position, direction);
            Map.Move(this, Position, newPosition, direction);
            LastAction = Action.Go;
            LastMove = direction;
            Position = newPosition;
        }
        else if (!IsInBattle)
        {
            direction = GetBestMove();
            newPosition = Map.Next(Position, direction);
            Map.Move(this, Position, newPosition, direction);
            LastAction = Action.Go;
            LastMove = direction;
            Position = newPosition;
        } else if(IsInBattle)
        {
            SimulationHistory.AddAction($"{this} is stuck in battle!");
        }
        if (Position == Target?.Position)
        {
            IsInBattle = true;
            Target.IsInBattle = true;
            Target.Target = this;
            LastAction = Action.Attack;
            BattleHandler.Battle(this, Target);
        }

    }
    public bool TakeDamage(int damage)
    {
        Health -= damage;
        if (IsDead)
        {
            //Console.WriteLine($"{this} PRZEGRYWA");
            Target.Target = null;
            Target = null;
            Map?.Remove(Position, this);
            
            return true;
        }
        return false;
    }
    public Direction GetBestMove()
    {
        Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
        Direction direction = directions[0];
        float shortestDistance = float.MaxValue;

        foreach (Direction dir in directions)
        {
            Point newPosition = Map?.Next(Position, dir) ?? Position;
            float dist = Map?.GetDistance(newPosition, Target?.Position ?? Position) ?? 0;

            if (dist < shortestDistance)
            {
                direction = dir;
                shortestDistance = dist;
            }
        }
        return direction;
    }
    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";

    public IMappable Clone()
    {
        return this.DeepClone(); // Use Force.DeepCloner here
    }
}

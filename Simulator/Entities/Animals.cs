using Force.DeepCloner;
using Simulator.Maps;
using Simulator.Utilities;
using System.Drawing;
using Action = Simulator.Utilities.Action;
using Point = Simulator.Utilities.Point;
namespace Simulator.Entities;

public class Animals : IMappable
{
    private string _description = "Unknown";
    public Faction Faction { get; } = Faction.Animal;
    public virtual char Symbol => char.ToUpper(GetType().Name[0]);
    public required string Description
    {
        get { return _description; }
        init => _description = Validator.Shortener(value, 3, 15, '#');
    }
    public uint Size { get; set; } = 3;
    public bool IsDead => Health <= 0;
    public virtual string Info => $"{Description} <{Size}>";
    public bool IsInBattle { get; set; }
    public Point Position { get; protected set; }
    public Point LastPosition { get; set; }
    public Map? Map { get; private set; }
    public Action LastAction { get; set; }
    public Direction LastMove { get; set; }
    public string? LogInfo { get; set; }
    public IMappable? Target { get; set; }
    public int Health { get; set; } = 10;

    public int Power => 2 * (int)Size;

    public virtual void Go()
    {
        LastPosition = Position;
        if (Map == null)
            throw new InvalidOperationException("Stwór nie jest przypisany do mapy.");

        Random rand = new Random();
        Direction direction;
        LastAction = Action.Go;
        Point newPosition;
        if (Target != null && Target.IsDead)
        {
            Target = null;
            IsInBattle = false;
        }

        if (Target == null)
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
            direction = (Direction)rand.Next(4);
            newPosition = Map.Next(Position, direction);
            Map.Move(this, Position, newPosition, direction);
            LastAction = Action.Go;
            LastMove = direction;
            Position = newPosition;
        }
        else if (IsInBattle)
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
    public void InitMapAndPosition(Map map, Point position)
    {
        if (!map.Exist(position))
            throw new ArgumentException("Pozycja spoza zakresu mapy.", nameof(position));
        if (Map != null)
            throw new InvalidOperationException($"Zwierze {Description} jest już przypisany do mapy i nie może zostać przeniesiony na inną mapę.");
        Map = map;
        Position = position;
    }
    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";
    public IMappable Clone()
    {
        return this.DeepClone(); // Use Force.DeepCloner here
    }

    public bool TakeDamage(int damage)
    {
        Health -= damage;
        if (IsDead)
        {
            Size--;
            //Console.WriteLine($"{this} PRZEGRYWA");
            Target.Target = null;
            Target = null;
            IsInBattle = false;
            if(Size == 0)
            {
                Map?.Remove(Position, this);
            } else
            {
                Health = 10;
            }
             // tutaj dac base health
            return true;
        }
        return false;
    }
}

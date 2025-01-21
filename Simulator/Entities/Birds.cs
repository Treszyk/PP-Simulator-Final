using Simulator.Maps;
using Simulator.Utilities;
using Action = Simulator.Utilities.Action;

namespace Simulator.Entities;

public class Birds : Animals
{
    public bool CanFly { get; init; } = true;
    public override char Symbol => CanFly ? 'B' : 'b';
    public override string Info => $"{Description} (fly{(CanFly ? '+' : '-')}) <{Size}>";
    override public void Go()
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

        if (Target == null || !IsInBattle)
        {
            LastPosition = Position;
            direction = (Direction)rand.Next(4);
            LastMove = direction;
            LastAction = Utilities.Action.Go;

            Point expectedPosition = !CanFly ? Position.NextDiagonal(direction) : Position.Next(direction);
            newPosition = !CanFly ? Map.NextDiagonal(Position, direction) : Map.Next(Position, direction);
            if ((expectedPosition.X != newPosition.X || expectedPosition.Y != newPosition.Y) && Map is BigBounceMap)
            {
                direction = MapMovement.BouncedDirection(direction);
            }
            if (CanFly) newPosition = Map.Next(newPosition, direction);
            Map.Move(this, Position, newPosition, direction);
            Position = newPosition;
            Console.WriteLine($"{direction} {this} {LastPosition} {Position} {newPosition}");
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
}

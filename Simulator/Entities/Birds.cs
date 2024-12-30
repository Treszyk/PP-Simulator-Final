using Simulator.Maps;
using Simulator.Utilities;

namespace Simulator.Entities;

public class Birds : Animals
{
    public bool CanFly { get; init; } = true;
    public override char Symbol => CanFly ? 'B' : 'b';
    public override string Info => $"{Description} (fly{(CanFly ? '+' : '-')}) <{Size}>";
    override public void Go()
    {
        if (Map == null)
            throw new InvalidOperationException("Stwór nie jest przypisany do mapy.");

        Random rand = new Random();
        Direction direction = (Direction)rand.Next(4);
        LastMove = direction;

        Point expectedPosition = !CanFly ? Position.NextDiagonal(direction) : Position.Next(direction);
        Point newPosition = !CanFly ? Map.NextDiagonal(Position, direction) : Map.Next(Position, direction);
        if ((expectedPosition.X != newPosition.X || expectedPosition.Y != newPosition.Y) && Map is BigBounceMap)
        {
            direction = MapMovement.BouncedDirection(direction);
        }
        if (CanFly) newPosition = Map.Next(newPosition, direction);
        Map.Move(this, Position, newPosition, direction);
        Position = newPosition;
    }
}

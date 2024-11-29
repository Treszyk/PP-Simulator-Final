namespace Simulator;

public class Birds : Animals
{
    public bool CanFly { get; init; } = true;
    public override char Symbol => CanFly ? 'B' : 'b';
    public override string Info => $"{Description} (fly{(CanFly ? '+' : '-')}) <{Size}>";
    override public void Go(Direction direction)
    {
        if (Map == null)
            throw new InvalidOperationException("Stwór nie jest przypisany do mapy.");

        Point newPosition = !CanFly ? Map.NextDiagonal(Position, direction) : Map.Next(Position, direction);
        if(CanFly) newPosition = Map.Next(newPosition, direction);
        Map.Move(this, Position, newPosition, direction);
        Position = newPosition;
    }
}

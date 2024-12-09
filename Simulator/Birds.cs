using Simulator.Maps;

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
        Point expectedPosition = Position.Next(direction);
        Point newPosition = !CanFly ? Map.NextDiagonal(Position, direction) : Map.Next(Position, direction);
        if ((expectedPosition.X != newPosition.X || expectedPosition.Y != newPosition.Y) && Map is BigBounceMap)
        {
            //nie wiedzialem do konca jak ma wygladac odbijanie sie ptakow latających
            //zrobilem tak ze jezeli są na samej krawędzi to odbijają się o 2 w przeciwną stronę
            //jezeli są o 1 od krawędzi to najpierw idą jedno pole w lewo a potem odbijają się o 1 w prawo
            //czyli ostatecznie w tym przypadku się nie ruszają
            direction = BigBounceMap.BouncedDirection(direction);
        }
        if(CanFly) newPosition = Map.Next(newPosition, direction);
        Map.Move(this, Position, newPosition, direction);
        Position = newPosition;
    }
}

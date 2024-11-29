namespace Simulator.Maps;

public interface IMappable
{
    Point Position { get; }
    void Go(Direction direction);
    void InitMapAndPosition(Map map, Point position);
}

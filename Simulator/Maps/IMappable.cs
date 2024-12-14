namespace Simulator.Maps;

public interface IMappable
{
    char Symbol => char.ToUpper(GetType().Name[0]);
    Point Position { get; }
    Map? Map { get; }
    void Go(Direction direction);
    void InitMapAndPosition(Map map, Point position);
    public string ToString();
}

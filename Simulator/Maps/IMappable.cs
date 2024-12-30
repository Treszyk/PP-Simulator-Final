using Simulator.Utilities;

namespace Simulator.Maps;

public interface IMappable
{
    char Symbol => char.ToUpper(GetType().Name[0]);
    Point Position { get; }
    bool IsInBattle { get; set; }
    bool IsDead { get; }
    public IMappable? Target { get; set; }
    public int Health { get; set; }
    Map? Map { get; }
    Direction LastMove { get; protected set; }
    void Go();
    void InitMapAndPosition(Map map, Point position);
    public string ToString();
}

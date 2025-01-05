using Simulator.Utilities;
using Action = Simulator.Utilities.Action;

namespace Simulator.Maps;

public interface IMappable
{
    char Symbol => char.ToUpper(GetType().Name[0]);
    Point Position { get; }
    Point LastPosition { get; set; }
    bool IsInBattle { get; set; }
    bool IsDead { get; }
    public IMappable? Target { get; set; }
    public int Health { get; set; }
    Map? Map { get; }
    public Action LastAction { get; protected set; }
    public Direction LastMove { get; protected set; }
    void Go();
    void InitMapAndPosition(Map map, Point position);
    public string ToString();
}

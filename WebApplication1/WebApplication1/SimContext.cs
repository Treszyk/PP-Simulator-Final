using Simulator;
using Simulator.Entities;
using Simulator.Maps;
using Simulator.Utilities;
namespace SimWeb;

public static class SimContext
{
    public static readonly Simulation SimInstance;
    public static readonly SimulationHistory SimHistoryInstance;
    static SimContext()
    {
        BigBounceMap map = new(8, 6);
        List<IMappable> mappables = [new Animals() { Description = "Króliki" }, new Elf("Elandor"), new Orc("Gorbag"), new Birds() { Description = "Orły", CanFly = true }, new Birds() { Description = "Strusie", CanFly = false }];
        if (mappables[2] is Creature cr)
        {
            cr.Target = mappables[1];
            if (mappables[1] is Creature cr2)
                cr2.Target = cr;
        }
        List<Point> points = [new(1, 1), new(0, 4), new(0, 1), new(0, 3), new(1, 3)];
        string moves = "";
        SimInstance = new(map, mappables, points, moves);
        SimHistoryInstance = new(SimInstance);
    }
}

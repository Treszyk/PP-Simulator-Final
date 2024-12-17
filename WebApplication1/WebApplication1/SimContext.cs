using Simulator;
using Simulator.Maps;
namespace SimWeb;

public static class SimContext
{
    public static readonly Simulation SimInstance;
    public static readonly SimulationHistory SimHistoryInstance;
    static SimContext()
    {
        BigBounceMap map = new(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Animals() { Description = "Króliki" }, new Birds() { Description = "Orły", CanFly = true }, new Birds() { Description = "Strusie", CanFly = false }];
        List<Point> points = [new(0, 0), new(4, 4), new(1, 1), new(1, 3), new(7, 5)];
        string moves = "ludludulurlrluulddrl";
        SimInstance = new(map, mappables, points, moves);
        SimHistoryInstance = new(SimInstance);
    }
}

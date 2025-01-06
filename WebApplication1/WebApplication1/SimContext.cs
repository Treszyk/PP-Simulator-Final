using Simulator;
using Simulator.Entities;
using Simulator.Maps;
using Simulator.Utilities;
namespace SimWeb;

public static class SimContext
{
    public static readonly Simulation SimInstance;
    public static readonly SimulationHistory SimHistoryInstance;
    public static readonly List<List<string>> AllActionsByTurn = SimulationHistory.turnActions;
    static SimContext()
    {
        BigBounceMap map = new(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag") { Level = 2 }, new Orc("Gorbag2"), new Orc("Gorbag3"), new Orc("Gorbag4"),
                                     new Elf("Elandor") { Level = 2 }, new Elf("Elandor2") { Level = 1 }, new Elf("Elandor3") { Level = 1 }, new Elf("Elandor4") { Level = 1 }, new Rabbit() { Description = "Króliki" }, new Birds() { Description = "Orły", CanFly = true }, new Birds() { Description = "Strusie", CanFly = false }];
        //if (mappables[2] is Creature cr)
        //{
        //    cr.Target = mappables[1];
        //    if (mappables[1] is Creature cr2)
        //        cr2.Target = cr;
        //}
        //if (mappables[5] is Creature cr3)
        //{
        //    cr3.Target = mappables[1];
        //}
        List<Point> points = [new(0, 0), new(0, 1), new(1, 0), new(1, 1), new(map.SizeX - 1, map.SizeY - 1), new(map.SizeX - 2, map.SizeY - 1),
                              new(map.SizeX - 1, map.SizeY - 2), new(map.SizeX-2, map.SizeY - 2), new(1, 3), new(3, 3), new(3, 3)];//dodac ze co 25 tur mozna przewinac
        string moves = "";
        SimInstance = new(map, mappables, points, moves);
        SimHistoryInstance = new(SimInstance);
    }
}

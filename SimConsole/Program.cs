using Simulator;
using Simulator.Maps;

namespace SimConsole;

class Program
{
    static void Main()
    {
        BigBounceMap map = new BigBounceMap(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Animals() { Description = "Króliki" }, new Birds() { Description = "Orły", CanFly = true }, new Birds() { Description = "Strusie", CanFly = false }];
        List<Point> points = [new(0, 0), new(4, 4), new(1, 1), new(1, 3), new(7, 5)];
        string moves = "ludludulurlrluulddrl";
        Simulation simulation = new(map, mappables, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);
        Console.WriteLine("SIMULATION!");
        Console.WriteLine("\nStarting positions:");

        while (!simulation.Finished)
        {
            Console.Write(mapVisualizer.Draw());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            simulation.Turn();
        }

        Console.Write(mapVisualizer.Draw());
        Console.WriteLine("End of simulation!");
    }
}

using Simulator;
using Simulator.Maps;
using System.Text;

namespace SimConsole;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        SmallSquareMap map = new(5, 5);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor")];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        Simulation simulation = new(map, mappables, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);
        Console.WriteLine("SIMULATION!");
        Console.WriteLine("\nStarting positions:");
        while (!simulation.Finished)
        {
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            simulation.Turn();
        }
        mapVisualizer.Draw();
        Console.WriteLine("End of simulation!");
    }
}

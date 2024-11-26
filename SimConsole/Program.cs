using Simulator;
using Simulator.Maps;
using System.Text;

namespace SimConsole;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        SmallSquareMap map = new(12, 5);
        List<Creature> creatures = [new Orc("Gorbag"), new Elf("Elandor"), new Elf("Legolas"), new Orc("Thrall"), new Elf("Sylvanas")];
        List<Point> points = [new(0, 0), new(3, 1), new(0, 0), new(1, 1), new(0, 0)];
        string moves = "dlrludlxxl";

        Simulation simulation = new(map, creatures, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);
        Console.WriteLine("SIMULATION!");
        Console.WriteLine("\nStarting positions:");
        while (!simulation.Finished)
        {
            mapVisualizer.Draw();

            Console.WriteLine("Press any key to continue...");
            var key = Console.ReadKey();
            simulation.Turn();
        }
        mapVisualizer.Draw();
    }
}

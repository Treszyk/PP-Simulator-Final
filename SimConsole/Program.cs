﻿using Simulator;
using Simulator.Maps;
using Simulator.Utilities;
using System.Text.Json.Serialization.Metadata;

namespace SimConsole;

class Program
{
    static void Main()
    {
        BigBounceMap map = new(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Animals() { Description = "Króliki" }, new Birds() { Description = "Orły", CanFly = true }, new Birds() { Description = "Strusie", CanFly = false }];
        List<Point> points = [new(0, 0), new(4, 4), new(1, 1), new(1, 3), new(7, 5)];
        string moves = "ludludulurlrluulddrl";
        Simulation simulation = new(map, mappables, points, moves);
        Console.WriteLine("SIMULATION!");
        //Console.WriteLine("\nStarting positions:");

        SimulationHistory sh = new(simulation);
        LogVisualizer lv = new(sh);
        for (int i = 0; i < sh.TurnLogs.Count; i++)
            Console.WriteLine($"{i}: {lv.Draw(i)}");
        //sh.DisplayTurn(0);
        
        //sh.DisplayHistory();

        //Console.Write(mapVisualizer.Draw());
        //Console.WriteLine("End of simulation!");
    }
}

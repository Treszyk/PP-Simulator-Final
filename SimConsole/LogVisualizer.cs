using Simulator.Maps;
using Simulator.Simulation;
using System.Text;

namespace Simulator;

public class LogVisualizer
{
    SimulationHistory Log { get; }
    public LogVisualizer(SimulationHistory log)
    {
        Log = log;
    }

    public string Draw(int turnIndex)
    {
        int width = Log.SizeX;
        int height = Log.SizeY;
        SimulationTurnLog turnLog = Log.TurnLogs[turnIndex];
        string output = $"{turnLog.Mappable}" + (turnIndex != 0 ? $" goes {turnLog.Move}\n" : "\n");

        output += Box.TopLeft;

        for (int x = 0; x < width; x++)
        {
            output += $"{Box.Horizontal}{(x < width - 1 ? Box.TopMid : "")}";
        }

        output += $"{Box.TopRight}\n";

        for (int y = height - 1; y >= 0; y--)
        {
            output += Box.Vertical;
            for (int x = 0; x < width; x++)
            {
                char mapChar = turnLog.Symbols.TryGetValue(new Point(x, y), out var val) ? val : ' ';
                output += $"{mapChar}{Box.Vertical}";
            }
            output += "\n";

            if (y > 0)
            {
                output += Box.MidLeft;
                for (int x = 0; x < width; x++) output += $"{Box.Horizontal}{(x < width - 1 ? Box.Cross : "")}";
                output += $"{Box.MidRight}\n";
            }
        }

        output += Box.BottomLeft;
        for (int x = 0; x < width; x++) output += $"{Box.Horizontal}{(x < width - 1 ? Box.BottomMid : "")}";
        output += $"{Box.BottomRight}\n";
        return output;
    }
}

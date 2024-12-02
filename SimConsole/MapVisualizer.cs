using Simulator.Maps;
using System.Text;

namespace Simulator;

public class MapVisualizer
{
    private readonly Map _map;

    public MapVisualizer(Map map)
    {
        _map = map;
        Console.OutputEncoding = Encoding.UTF8;
    }

    public string Draw()
    {
        int width = _map.SizeX;
        int height = _map.SizeY;
        string output = "";

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
                var mappables = _map.At(new Point(x, y));
                char displayChar = mappables.Count switch
                {
                    0 => ' ',
                    1 => mappables[0].Symbol,
                    _ => 'X'
                };
                output += $"{displayChar}{Box.Vertical}";
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

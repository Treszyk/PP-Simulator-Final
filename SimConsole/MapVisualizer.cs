using Simulator;
using Simulator.Maps;
using System.Drawing;
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

    public void Draw()
    {
        int width = _map.SizeX;
        int height = _map.SizeY;

        Console.Write(Box.TopLeft);

        for (int x = 0; x < width; x++)
        {
            Console.Write($"{Box.Horizontal}{(x < width - 1 ? Box.TopMid : "")}");
        }

        Console.WriteLine(Box.TopRight);

        for (int y = height - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < width; x++)
            {
                var mappables = _map.At(new Point(x, y));
                char displayChar = mappables.Count switch
                {
                    0 => ' ',
                    1 => mappables[0].Symbol,
                    _ => 'X'
                };
                Console.Write($"{displayChar}{Box.Vertical}");
            }
            Console.WriteLine();

            if (y > 0)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < width; x++) Console.Write($"{Box.Horizontal}{(x < width - 1 ? Box.Cross : "")}");
                Console.WriteLine(Box.MidRight);
            }
        }

        Console.Write(Box.BottomLeft);
        for (int x = 0; x < width; x++) Console.Write($"{Box.Horizontal}{(x < width - 1 ? Box.BottomMid : "")}");
        Console.WriteLine(Box.BottomRight);
    }
}

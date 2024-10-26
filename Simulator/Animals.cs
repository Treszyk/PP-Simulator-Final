using System.Reflection.Emit;
using System.Xml.Linq;

namespace Simulator;

public class Animals
{
    public required string Description { get; init; }
    public uint Size { get; set; } = 3;
    public string Info => $"{Description} [{Size}]";
}

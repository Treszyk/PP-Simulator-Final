using System.Xml.Linq;

namespace Simulator.Utilities;

public static class Validator
{
    public static int Limiter(int value, int min, int max)
    {
        return Math.Clamp(value, min, max);
    }

    public static string Shortener(string value, int min, int max, char placeholder)
    {
        value = value.Trim();
        if (value.Length > max)
            value = value.Remove(max).Trim();
        if (value.Length < min)
            value = value.PadRight(min, placeholder);
        if (char.IsLower(value[0]))
            value = char.ToUpper(value[0]) + value.Substring(1);
        return value;
    }
}

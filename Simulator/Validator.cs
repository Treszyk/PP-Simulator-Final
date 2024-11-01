using System.Xml.Linq;

namespace Simulator;

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
            value = value.Remove(25).Trim();
        if (value.Length < min)
            value = value.PadRight(3, placeholder);
        if (char.IsLower(value[0]))
            value = char.ToUpper(value[0]) + value.Substring(1);
        return value;
    }
}

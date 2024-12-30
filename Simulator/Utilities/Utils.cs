using Simulator.Maps;

namespace Simulator.Utilities;

public static class Utils
{
    public static int UpdateIndexValue(int value, int maxIndex)
    {
        if (value >= maxIndex)
            return 0; 
        return value;
    }
}

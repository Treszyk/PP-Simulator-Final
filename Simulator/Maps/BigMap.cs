namespace Simulator.Maps;

public abstract class BigMap : Map
{
    public BigMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 1000 || sizeY > 1000)
            throw new ArgumentOutOfRangeException($"Wymiary mapy nie mogą przekraczać 1000x1000. Twoje wymiary: {sizeX}x{sizeY}");
    }
}

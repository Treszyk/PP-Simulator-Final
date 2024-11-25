namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20 || sizeY > 20)
            throw new ArgumentOutOfRangeException("Wymiary mapy nie mogą przekraczać 20x20. Twoje wymiary: {sizeX}x{sizeY}");
    }
}

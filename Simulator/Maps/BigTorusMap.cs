namespace Simulator.Maps;

public class BigTorusMap : BigMap
{
    public BigTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        FNext = MapMovement.TorusNext;
        FNextDiagonal = MapMovement.TorusNextDiagonal;
    }
}

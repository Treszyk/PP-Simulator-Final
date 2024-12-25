namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public int Size { get; }
    public SmallSquareMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        FNext = MapMovement.WallNext;
        FNextDiagonal = MapMovement.WallNextDiagonal;
    }
}

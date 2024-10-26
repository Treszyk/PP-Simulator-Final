namespace Simulator;

public static class DirectionParser
{
    public static Direction[] Parse(string input)
    {
        //mozna to listami zrobic prosciej, ale w zadaniu są tablice
        //to zrobilem tablicami
        input = input.ToUpper();
        int count = 0;
        Direction[] directions = new Direction[input.Length];
        foreach (char letter in input)
        {
            switch (letter)
            {
                case 'U':
                    directions[count++] = Direction.Up; break;
                case 'R':
                    directions[count++] = Direction.Right; break;
                case 'D':
                    directions[count++] = Direction.Down; break;
                case 'L':
                    directions[count++] = Direction.Left; break;
            }
        }
        Array.Resize(ref directions, count);
        return directions;
    }
}

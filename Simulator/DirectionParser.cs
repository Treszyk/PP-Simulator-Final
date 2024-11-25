namespace Simulator;

public static class DirectionParser
{
    public static List<Direction> Parse(string input)
    {
        input = input.ToUpper();
        List<Direction> directions = [];
        foreach (char letter in input)
        {
            switch (letter)
            {
                case 'U':
                    directions.Add(Direction.Up); break;
                case 'R':
                    directions.Add(Direction.Right); ; break;
                case 'D':
                    directions.Add(Direction.Down); break;
                case 'L':
                    directions.Add(Direction.Left); break;
            }
        }
        return directions;
    }
}

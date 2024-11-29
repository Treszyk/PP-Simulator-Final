namespace Simulator;
using Simulator.Maps;


public class Simulation
{
    private List<Direction> _directions;
    private int _currentTurnIndex = 0;
    public Map Map { get; }
    public List<IMappable> Mappables { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished = false;
    public IMappable CurrentMappable => Mappables[_currentTurnIndex % Mappables.Count];
    public string CurrentMoveName => _directions[_currentTurnIndex % _directions.Count].ToString().ToLower();
    public Simulation(Map map, List<IMappable> mappables,
        List<Point> positions, string moves)
    {
        if (mappables == null || !mappables.Any())
            throw new ArgumentException("Mappables list cannot be empty.");

        if (mappables.Count != positions.Count)
            throw new ArgumentException("Number of mappables must match the number of starting positions.");

        if (moves.Length == 0)
            Finished = true;

        Moves = moves;
        Map = map;
        Mappables = mappables;
        Positions = positions;
        _directions = DirectionParser.Parse(moves);

        for (int i = 0; i < mappables.Count; i++)
        {
            map.Add(positions[i], mappables[i]);
            mappables[i].InitMapAndPosition(map, positions[i]);
        }
    }
    public void Turn() 
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is finished.");

        Console.WriteLine($"\nTurn {_currentTurnIndex + 1}");
        Console.WriteLine($"{CurrentMappable} {CurrentMappable.Position} goes {CurrentMoveName}:");
        CurrentMappable.Go(_directions[_currentTurnIndex % _directions.Count]);
        
        _currentTurnIndex++;

        if (_currentTurnIndex >= _directions.Count)
            Finished = true;
    }
}
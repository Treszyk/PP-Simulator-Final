namespace Simulator;
using Simulator.Maps;


public class Simulation
{
    private int _currentTurnIndex = 0;
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<Creature> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves, 
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature CurrentCreature => Creatures[_currentTurnIndex % Creatures.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName => Moves[_currentTurnIndex % Moves.Length].ToString().ToLower();

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<Creature> creatures,
        List<Point> positions, string moves)
    {
        if (creatures == null || !creatures.Any())
            throw new ArgumentException("Creatures list cannot be empty.");

        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures must match the number of starting positions.");

        Map = map;
        Creatures = creatures;
        Positions = positions;
        Moves = string.Join("", DirectionParser.Parse(moves));

        for (int i = 0; i < creatures.Count; i++)
        {
            map.Add(creatures[i], positions[i]);
            creatures[i].Position = positions[i];
        }

    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn() 
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is finished.");

        var currentMove = CurrentMoveName;
        var direction = DirectionParser.Parse(currentMove);

        if (direction != null)
        {
            var currentCreature = CurrentCreature;
            var from = currentCreature.Position;
            var to = Map.Next(from, direction[0]);

            Map.Move(currentCreature, from, to);
            currentCreature.Position = to;
        }

        _currentTurnIndex++;

        if (_currentTurnIndex >= Moves.Length)
            Finished = true;
    }
}
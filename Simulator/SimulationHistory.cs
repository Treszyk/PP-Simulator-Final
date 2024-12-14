using Simulator;
using Simulator.Maps;

public class SimulationHistory
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        //first frame
        SimulationTurnLog simulationTurnLog = new() { 
            Mappable = $"STARTING POSITIONS", 
            Move = "", 
            Symbols = GetPositionChars() 
        };
        TurnLogs.Add(simulationTurnLog);

        while (!_simulation.Finished)
        {
            IMappable currentMappable = _simulation.CurrentMappable;
            Point currentMappablePosition = currentMappable.Position;
            string currentMoveName = _simulation.CurrentMoveName;

            _simulation.Turn();

            simulationTurnLog = new()
            {
                Mappable = $"{currentMappable} {currentMappablePosition}",
                Move = currentMoveName,
                Symbols = GetPositionChars()
            };

            TurnLogs.Add(simulationTurnLog);
        }
    }
    private Dictionary<Point, char> GetPositionChars()
    {
        Dictionary<Point, List<IMappable>> positions = _simulation.Map.MappablePositions;
        Dictionary<Point, char> PositionChars = [];
        foreach (KeyValuePair<Point, List<IMappable>> entry in positions)
        {
            PositionChars[entry.Key] = entry.Value.Count switch
            {
                0 => ' ',
                1 => entry.Value[0].Symbol,
                _ => 'X'
            };
        }
        return PositionChars;
    }
}
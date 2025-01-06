using Simulator;
using Simulator.Maps;
using Point = Simulator.Utilities.Point;
using Action = Simulator.Utilities.Action;
using Force.DeepCloner;

public class SimulationHistory
{
    public static readonly List<List<string>> turnActions = [];
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        turnActions.Clear();
        //turnActions.Add(["STARTING POSITIONS"]);
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
            Symbols = GetPositionChars(),
            TileLogs = GetTileLogs()
        };
        TurnLogs.Add(simulationTurnLog);

        while (!_simulation.Finished)
        {
            IMappable currentMappable = _simulation.CurrentMappable;
            Point currentMappablePosition = currentMappable.Position;
            //string currentMoveName = _simulation.CurrentMoveName;

            //Console.WriteLine($"SIM LOG CURR MAPPABLE {currentMappable} PRZED {_simulation.CurrentMappable}");
            AddNewActions();
            _simulation.Turn();
            string MappableMove;
            if(currentMappable.LastAction == Action.Regen)
            {
                MappableMove = $" stands still and regenerates {(int)(0.2*15)} health!";//base health
            } else
            {
                MappableMove = (currentMappable.LastPosition != currentMappable.Position) ? " goes " + currentMappable.LastMove.ToString().ToLower() : " doesn't move!";
            }
            simulationTurnLog = new()
            {
                Mappable = $"{currentMappable} {currentMappablePosition}",
                Move = MappableMove,
                LogInfo = "",
                Symbols = GetPositionChars(),
                TileLogs = GetTileLogs()
            };


            if (simulationTurnLog.Move != "")
                turnActions[^1].Insert(0, simulationTurnLog.Mappable + simulationTurnLog.Move);

            TurnLogs.Add(simulationTurnLog);
            //DisplayActions();
            //Console.WriteLine($"SIM LOG CURR MAPPABLE {currentMappable} PO {_simulation.CurrentMappable}");
        }
        if(_simulation.Winner != null)
            AddAction($"And with that the {_simulation.Winner} faction wins!");
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
    private Dictionary<Point, List<IMappable>> GetTileLogs()
    {
        Dictionary<Point, List<IMappable>> TileLogs = [];
        foreach (Point point in _simulation.Map.MappablePositions.Keys)
        {
            TileLogs.Add(point, new List<IMappable>(_simulation.Map.MappablePositions[point]).DeepClone());
            //Console.WriteLine(_simulation.Map.MappablePositions[point][0]);
        }
        return TileLogs;
    }
    public static void AddNewActions() => turnActions.Add([]);
    public static void AddAction(string action) => turnActions[^1].Add(action);
    public void Display()
    {
        int turnNum = 1;
        foreach(var turn in turnActions)
        {
            Console.WriteLine($"\n\nTura: {turnNum++}");
            foreach (var action in turn)
            {
                Console.WriteLine(action);
            }
            Console.WriteLine("\n\n");
        }
    }
}
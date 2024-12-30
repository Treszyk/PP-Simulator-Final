namespace Simulator;

using Simulator.Entities;
using Simulator.Maps;
using Simulator.Utilities;

public class Simulation
{
    private List<Direction> _directions;
    private int _currentTurnIndex = 0;
    public int CurrentMappableIndex { get; set; }
    public Map Map { get; }
    public List<IMappable> Mappables { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished = false;
    public IMappable CurrentMappable => Mappables[CurrentMappableIndex];//_currentTurnIndex % Mappables.Count
    //public string CurrentMoveName => _directions[_currentTurnIndex % _directions.Count].ToString().ToLower();
    public string CurrentMoveName { get; set; }
    public Simulation(Map map, List<IMappable> mappables,
        List<Point> positions, string moves)
    {
        if (mappables == null || !mappables.Any())
            throw new ArgumentException("Mappables list cannot be empty.");

        if (mappables.Count != positions.Count)
            throw new ArgumentException("Number of mappables must match the number of starting positions.");

        //if (moves.Length == 0)
        //    Finished = true;

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
    public int FindDeadIndex()
    {
        for (int i = 0; i < Mappables.Count; i++)
        {
            if (Mappables[i].Health <= 0)
            {
                return i;
            }
        }
        return -1;
    }
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is finished.");
        //Console.WriteLine($"\nTurn {_currentTurnIndex + 1}");
        //Console.WriteLine($"{CurrentMappable} {CurrentMappable.Position} goes {CurrentMoveName}:");
        //CurrentMappable.Go(_directions[_currentTurnIndex % _directions.Count]);
        Console.WriteLine($"{CurrentMappable is Creature} {_currentTurnIndex} {_directions.Count}");
        CurrentMappable.Go();
        Console.WriteLine($"{CurrentMappable} TARGET: {CurrentMappable.Target} dead? {CurrentMappable.IsDead}");

        if (FindDeadIndex() is var deadIndex && deadIndex != - 1)
        {
            IMappable dead = Mappables[deadIndex];
            if(deadIndex > CurrentMappableIndex)
            {
                CurrentMappableIndex += 1;
            }
            Mappables.Remove(dead);
            //CurrentMappableIndex = CurrentMappableIndex;
        } else
        {
            CurrentMappableIndex += 1;
        }
        CurrentMappableIndex = Utils.UpdateIndexValue(CurrentMappableIndex, Mappables.Count);
        Console.WriteLine($"INDEKS: {_currentTurnIndex % Mappables.Count} {CurrentMappable} {_currentTurnIndex}");

        _currentTurnIndex++;
        if (_currentTurnIndex == 50)
            Finished = true;
    }
}
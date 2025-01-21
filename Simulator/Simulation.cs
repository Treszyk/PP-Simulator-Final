namespace Simulator;

using Microsoft.VisualBasic.FileIO;
using Simulator.Entities;
using Simulator.Maps;
using Simulator.Utilities;
using System.Linq;

public class Simulation
{
    private int _currentTurnIndex = 0;
    public int CurrentMappableIndex { get; set; }
    public Map Map { get; }
    public List<IMappable> Mappables { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished = false;
    public IMappable CurrentMappable => Mappables[CurrentMappableIndex];//_currentTurnIndex % Mappables.Count
    //public string CurrentMoveName => _directions[_currentTurnIndex % _directions.Count].ToString().ToLower();
    public List<IMappable> NoTargetList => Mappables.Where(mappable => mappable.Target == null).ToList();
    public int OrcQuantity => Mappables.OfType<Orc>().ToList().Count();
    public int ElfQuantity => Mappables.OfType<Elf>().ToList().Count();
    public int AnimalQuantity => Mappables.OfType<Animals>().ToList().Count();
    public Faction? Winner = null;
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

        for (int i = 0; i < mappables.Count; i++)
        {
            map.Add(positions[i], mappables[i]);
            mappables[i].InitMapAndPosition(map, positions[i]);
        }
        Random rng = new Random();
        Mappables = Mappables.OrderBy(_ => rng.Next()).ToList();
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
        //Console.WriteLine($"{CurrentMappable is Creature} {_currentTurnIndex} {_directions.Count}");
        Console.WriteLine("PRZED GO");
        CurrentMappable.Go();
        //Console.WriteLine($"{CurrentMappable} TARGET: {CurrentMappable.Target} dead? {CurrentMappable.IsDead}");

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
        //Console.WriteLine($"INDEKS: {_currentTurnIndex % Mappables.Count} {CurrentMappable} {_currentTurnIndex}");
        if ((_currentTurnIndex+1) % 3 == 0)
        {
            int attackWho = Random.Shared.Next(0, 100);
            if(attackWho < 50 && _currentTurnIndex > 80)
                SetTarget(typeof(Creature));
            else
                SetTarget(typeof(Animals));
        }

        _currentTurnIndex++;

        Console.WriteLine(_currentTurnIndex);
        CheckWinCondition();


        Console.WriteLine("PO TYM IFIE");

    }
    public void SetTarget(Type typ)
    {
        if(NoTargetList.Count > 1)
        {
            IMappable mappable = NoTargetList[0];
            Console.WriteLine($"{mappable.GetType()}");
            for (int i=1; i<NoTargetList.Count; i++)
            {
                IMappable scndMappable = NoTargetList[i];   
                if (mappable.Faction != scndMappable.Faction && typ.IsAssignableFrom(scndMappable.GetType()))
                {
                    Console.WriteLine("set");
                    mappable.Target = scndMappable;
                    scndMappable.Target = mappable;
                    break;
                }
            }
        }
    }
    public void CheckWinCondition()
    {
        if (OrcQuantity == 0 && ElfQuantity == 0 && AnimalQuantity > 0)
            Winner = Faction.Animal;
        else if (OrcQuantity == 0 && ElfQuantity > 0 && AnimalQuantity == 0)
            Winner = Faction.Elf;
        else if (OrcQuantity > 0 && ElfQuantity == 0 && AnimalQuantity == 0)
            Winner = Faction.Orc;
        else
            return;
        Finished = true;
    }
}
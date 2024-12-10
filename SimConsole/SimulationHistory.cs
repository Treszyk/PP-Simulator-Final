using Simulator.Maps;

namespace Simulator;

public class SimulationHistory
{
    private readonly Simulation _simulation;
    private readonly List<Tuple<Point, string>> _movements;
    private readonly List<IMappable> _mappables;
    private readonly List<string> _frames;
    private readonly MapVisualizer _visualizer;
    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation;
        _movements = [];
        _mappables = [];
        _frames = [];
        _visualizer = new(simulation.Map);
        
        RunSimulation();
    }

    public void RunSimulation()
    {
        //dodawanie startowej klatki symulacji
        _frames.Add(_visualizer.Draw());
        //dodawanie reszty klatek symulacji
        while (!_simulation.Finished)
        {
            _mappables.Add(_simulation.CurrentMappable);
            _movements.Add(new(_simulation.CurrentMappable.Position, _simulation.CurrentMoveName));
            _simulation.Turn();
            _frames.Add(_visualizer.Draw());
        }
    }
    //displays only one frame of the simulation
    public void DisplayTurn(int turn)
    {
        Console.WriteLine("SIMULATION!");
        if (turn - 1 == -1)
        {
            Console.WriteLine("\nStarting positions:");
            Console.WriteLine($"\nTurn {turn}");
            Console.WriteLine(_frames[turn]);
        }
        else
        {
            Console.WriteLine($"\nTurn {turn}");
            Console.WriteLine($"{_mappables[turn - 1]} {_movements[turn - 1].Item1} goes {_movements[turn - 1].Item2}:");
            Console.WriteLine(_frames[turn]);
        }
    }
    //allows the user to control what frame is displayed
    public void DisplayHistory(int turn = 0)
    {
        while(true)
        {
            DisplayTurn(turn);

            Console.WriteLine("\nType the index of the turn you would like to view or nothing to stop:");
            var uInput = Console.ReadLine();

            if (uInput == "")
            {
                Console.WriteLine("End of simulation!");
                break;
            } else
            {
                turn = Math.Clamp(Convert.ToInt32(uInput), 0, _mappables.Count);
            }
            
            Console.Clear();
        }
    }
}

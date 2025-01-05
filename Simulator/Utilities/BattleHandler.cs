using Simulator.Entities;
using Simulator.Maps;

namespace Simulator.Utilities;

public static class BattleHandler
{
    public static void Attack(Creature cr, Creature cr2)
    {
        SimulationHistory.AddAction($"{cr} atakuje {cr2} za {cr.Power}");
        if (!cr2.TakeDamage(cr.Power))
        {
            SimulationHistory.AddAction($"{cr2} atakuje {cr} za {cr2.Power}");
            if (cr.TakeDamage(cr2.Power))
                SimulationHistory.AddAction($"{cr} został pokonany przez {cr2}!");
        }
        else
        {
            SimulationHistory.AddAction($"{cr2} został pokonany przez {cr}!");
        }
    }
    public static void Battle(Creature cr, IMappable mp)
    {
        if (mp is Creature cr2)
        {
            int whoFirst = Random.Shared.Next(0, 100);
            SimulationHistory.AddAction($"Wartość whoFirst = {whoFirst}");
            if (whoFirst < 50)
                Attack(cr, cr2);
            else
                Attack(cr2, cr);
        }
    }
}

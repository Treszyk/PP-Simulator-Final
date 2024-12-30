using Simulator.Entities;
using Simulator.Maps;

namespace Simulator.Utilities;

public static class BattleHandler
{
    public static void Battle(Creature cr, IMappable mp)
    {
        Console.WriteLine($"{cr} | {mp}");
        if (mp is Creature cr2)
        {
            int whoFirst = Random.Shared.Next(0, 100);
            Console.WriteLine($"Wartość whoFirst = {whoFirst}");
            if (whoFirst < 50)
            {
                Console.WriteLine($"{cr} atakuje {cr2} za {cr.Power}");
                if (!cr2.TakeDamage(cr.Power))
                    cr.TakeDamage(cr2.Power);
            }
            else
            {
                Console.WriteLine($"{cr2} atakuje {cr} za {cr2.Power}");
                if (!cr.TakeDamage(cr2.Power))
                    cr2.TakeDamage(cr.Power);
            }
        }
    }
}

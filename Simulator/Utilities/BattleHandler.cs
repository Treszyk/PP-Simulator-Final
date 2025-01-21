using Simulator.Entities;
using Simulator.Maps;

namespace Simulator.Utilities;

public static class BattleHandler
{
    public static void Attack(IMappable im, IMappable im2)
    {
        SimulationHistory.AddAction($"{im} attacks {im2} for {im.Power} hp");
        if (!im2.TakeDamage(im.Power))
        {
            SimulationHistory.AddAction($"{im2} attacks {im} for {im2.Power} hp");
            if (im.TakeDamage(im2.Power))
            {
                SimulationHistory.AddAction($"{im} was defeated by {im2}!");
                im2.IsInBattle = false;
                im2.LevelUp();
            }
        }
        else
        {
            SimulationHistory.AddAction($"{im2} was defeated by {im}!");
            im.IsInBattle = false;
            im.LevelUp();
        }
    }
    public static void Battle(IMappable mp1, IMappable mp2)
    {
            int whoFirst = Random.Shared.Next(0, 100);
            
            if (whoFirst >= 50)
            {
                SimulationHistory.AddAction($"{mp1} rolls {whoFirst} and attacks first!");
                Attack(mp1, mp2);
            } else
            {
                SimulationHistory.AddAction($"{mp1} rolls {whoFirst}, {mp2} attacks first!");
                Attack(mp2, mp1);
            }            
    }
}

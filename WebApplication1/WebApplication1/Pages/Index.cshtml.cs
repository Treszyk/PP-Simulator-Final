using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using SimWeb;

public class IndexModel : PageModel
{
    public string? Moves { get; private set; }
    public string? MapName { get; private set; }
    public void OnGet()
    {
       Simulation sim = SimContext.SimInstance;
        // Counter = HttpContext.Session.GetInt32("Counter") ?? 1;
        Moves = sim.Moves;
        MapName = sim.Map.GetType().Name;
    }
    public void OnPost()
    {
        //Counter = HttpContext.Session.GetInt32("Counter") ?? 1;
        //Counter++;
        //HttpContext.Session.SetInt32("Counter", Counter);
    }
}
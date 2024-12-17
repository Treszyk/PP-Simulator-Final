using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using System.Diagnostics.Metrics;

namespace SimWeb.Pages;

public class SimModel : PageModel
{
    public SimulationHistory SimHistory = SimContext.SimHistoryInstance;
    public int TurnIndex { get; set; } = 0;
    public SimulationTurnLog TurnLog => SimHistory.TurnLogs[TurnIndex];
    public void OnGet()
    {
        TurnIndex = int.TryParse(Request.Cookies["TurnIndex"], out int index) ? index : HttpContext.Session.GetInt32("TurnIndex") ?? 0;
    }

    public void OnPost()
    {
        var action = Request.Form["action"];
        TurnIndex = int.TryParse(Request.Cookies["TurnIndex"], out int index) ? index : HttpContext.Session.GetInt32("TurnIndex") ?? 0;
        if (action == "increase")
        {
            TurnIndex++;
        }
        else if (action == "decrease")
        {
            TurnIndex--;
        }
        
        TurnIndex = Math.Clamp(TurnIndex, 0, SimHistory.TurnLogs.Count - 1);
        //TurnIndex = HttpContext.Session.GetInt32("TurnIndex") ?? 0;
        HttpContext.Session.SetInt32("TurnIndex", TurnIndex);
        Response.Cookies.Append("TurnIndex", $"{TurnIndex}");
    }
}

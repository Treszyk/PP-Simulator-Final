using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Utilities;

namespace SimWeb.Pages;

public class SimModel : PageModel
{
    public SimulationHistory SimHistory = SimContext.SimHistoryInstance;
    public int TurnIndex { get; set; } = 0;
    public SimulationTurnLog TurnLog => SimHistory.TurnLogs[TurnIndex];
    public List<List<string>> AllActionsByTurn => SimContext.AllActionsByTurn;
    public Point? SelectedPoint = null;
    public List<Simulator.Maps.IMappable>? IMappablesAtTile => 
        (SelectedPoint!=null && TurnLog.TileLogs.ContainsKey((Point)SelectedPoint)) ? TurnLog.TileLogs[(Point)SelectedPoint] : [];
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
    public void OnPostUpdateTileContext(int x, int y)
    {
        //dodac do turnLog pole z historycznymi mapami tile ale tylko nie puste trzymac w pamieci
        Console.WriteLine($"Handler invoked with x={x}, y={y}");
        SelectedPoint = new Point(x, y);
        // Process tile coordinates (x, y) and update context
        string TileInfo = $"You clicked on tile (, )"; // Example context info
        TurnIndex = int.TryParse(Request.Cookies["TurnIndex"], out int index) ? index : HttpContext.Session.GetInt32("TurnIndex") ?? 0;
    }
    public IActionResult OnGetTestHandler()
    {
        Console.WriteLine("Test handler invoked");
        return new JsonResult(new { success = true });
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Utilities;

namespace SimWeb.Pages;

public class SimModel : PageModel
{
    public Simulation SimInstance = SimContext.SimInstance;
    public SimulationHistory SimHistory = SimContext.SimHistoryInstance;
    public int TurnIndex { get; set; } = 0;
    public SimulationTurnLog TurnLog => SimHistory.TurnLogs[TurnIndex];
    public List<List<string>> AllActionsByTurn => SimContext.AllActionsByTurn;
    public Point? SelectedPoint = null;
    public List<Simulator.Maps.IMappable>? IMappablesAtTile => 
        (SelectedPoint!=null && TurnLog.TileLogs.ContainsKey((Point)SelectedPoint)) ? TurnLog.TileLogs[(Point)SelectedPoint] : [];
    public void OnGet()
    {
        TurnIndex = Math.Clamp(int.TryParse(Request.Cookies["TurnIndex"], out int index) ? index : HttpContext.Session.GetInt32("TurnIndex") ?? 0, 0, SimHistory.TurnLogs.Count - 1);
    }

    public void OnPost()
    {
        var action = Request.Form["action"];
        TurnIndex = int.TryParse(Request.Cookies["TurnIndex"], out int index) ? index : HttpContext.Session.GetInt32("TurnIndex") ?? 0;
        int? selectX = HttpContext.Session.GetInt32("cordX");
        int? selectY = HttpContext.Session.GetInt32("cordY");
        if (selectX != null && selectY != null)
        {
            SelectedPoint = new Point((int)selectX, (int)selectY);
        }
        if (action == "increase")
        {
            TurnIndex++;
        }
        else if (action == "decrease")
        {
            TurnIndex--;
        } else if(int.TryParse(action, out int n))
        {
            TurnIndex = n;
        }
        
        TurnIndex = Math.Clamp(TurnIndex, 0, SimHistory.TurnLogs.Count - 1);
        //TurnIndex = HttpContext.Session.GetInt32("TurnIndex") ?? 0;
        HttpContext.Session.SetInt32("TurnIndex", TurnIndex);
        Response.Cookies.Append("TurnIndex", $"{TurnIndex}");
    }
    public void OnPostUpdateTileContext(int x, int y)
    {
        TurnIndex = Math.Clamp(int.TryParse(Request.Cookies["TurnIndex"], out int index) ? index : HttpContext.Session.GetInt32("TurnIndex") ?? 0, 0, SimHistory.TurnLogs.Count - 1);
        int? selectX = HttpContext.Session.GetInt32("cordX");
        int? selectY = HttpContext.Session.GetInt32("cordY");
        Point newPoint = new Point(x, y);
        if(selectX == x && selectY == y)
        {
            Console.WriteLine("aha");
            SelectedPoint = null;
            HttpContext.Session.Remove("cordX");
            HttpContext.Session.Remove("cordY");
            return;
        }
        SelectedPoint = newPoint;
        HttpContext.Session.SetInt32("cordX", x); 
        HttpContext.Session.SetInt32("cordY", y);
    }
    public void OnPostSpecificTurnChosen(int turn)
    {

    }
    public IActionResult OnGetTestHandler()
    {
        Console.WriteLine("Test handler invoked");
        return new JsonResult(new { success = true });
    }
}

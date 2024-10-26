namespace Simulator;

public class Animals
{
    private string _description = "Unknown";
    public required string Description {
        get { return _description; }
        init
        {
            //Console.WriteLine(value.Length);
            _description = value.Trim();
            if (_description.Length > 15)
            {
                _description = _description.Remove(15);
                _description = _description.Trim();
            }
            if (_description.Length < 3)
                _description = _description.PadRight(3, '#');
            if (char.IsLower(_description[0]))
            {
                _description = char.ToUpper(_description[0]) + _description.Substring(1);
            }
        }
    }
    public uint Size { get; set; } = 3;
    public string Info => $"{Description} [{Size}]";
}

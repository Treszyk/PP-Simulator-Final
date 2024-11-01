namespace Simulator;

public class Birds : Animals
{
    public bool CanFly { get; init; } = true;
    public override string Info => $"{Description} (fly{(CanFly ? '+' : '-')}) <{Size}>";
}

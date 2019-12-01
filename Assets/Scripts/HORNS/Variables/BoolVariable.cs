using HORNS;

public class BoolVariable : DemoVariable<bool>
{
    public HORNS.BoolVariable Variable { get; } = new HORNS.BoolVariable();
    public override Variable<bool> LibVariable => Variable;
}
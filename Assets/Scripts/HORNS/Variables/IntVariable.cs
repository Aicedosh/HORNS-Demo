using HORNS;

public class IntVariable : DemoVariable<int>
{
    public HORNS.IntVariable Variable { get; } = new HORNS.IntVariable();
    public override Variable<int> LibVariable => Variable;
}
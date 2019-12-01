using HORNS;

public class BoolNeed : DemoNeed<bool>
{
    public BoolVariable Variable { get; }
    public override DemoVariable<bool> GenericVariable => Variable;

    public float TrueValue;
    public float FalseValue;

    protected override float Evaluate(bool value)
    {
        return value ? TrueValue : FalseValue;
    }


}

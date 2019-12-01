using HORNS;

public class LinearNeed : DemoNeed<int>
{
    public float ValueFactor;

    public IntVariable Variable;
    public override DemoVariable<int> GenericVariable => Variable;

    protected override float Evaluate(int value)
    {
        return ValueFactor * value;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

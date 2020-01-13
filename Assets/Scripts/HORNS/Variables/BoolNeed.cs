using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoolNeed : DemoNeed<bool>
{
    public BoolVariable Variable;
    public override DemoVariable<bool> GenericVariable => Variable;

    public float TrueValue;
    public float FalseValue;

    protected override float Evaluate(bool value)
    {
        return value ? TrueValue : FalseValue;
    }

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().BoolNeedPrefab;
        var go = Instantiate(canvas);
        go.GetComponent<BooleanNeedDisplay>().Need = this;
        return go;
    }
}

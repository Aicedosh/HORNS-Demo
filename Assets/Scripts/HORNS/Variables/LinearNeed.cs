using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LinearNeed : DemoNeed<int>
{
    public float ValueFactor;

    public IntVariable Variable;
    public override DemoVariable<int> GenericVariable => Variable;

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().IntNeedPrefab;
        var go = Instantiate(canvas);
        return go;
    }

    protected override float Evaluate(int value)
    {
        return ValueFactor * value;
    }
}

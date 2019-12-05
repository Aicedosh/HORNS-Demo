using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LogNeed : DemoNeed<int>
{
    public override DemoVariable<int> GenericVariable => Variable;

    public float Multiplier;

    public IntVariable Variable;

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().IntNeedPrefab;
        var go = Instantiate(canvas);
        go.GetComponent<IntegerNeedDisplay>().Need = this;
        return go;
    }

    protected override float Evaluate(int value)
    {
        return Multiplier * Mathf.Log10(value + 1);
    }
}

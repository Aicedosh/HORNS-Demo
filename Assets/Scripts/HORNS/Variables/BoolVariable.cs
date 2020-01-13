using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoolVariable : DemoVariable<bool>
{
    public HORNS.BooleanVariable Variable { get; } = new HORNS.BooleanVariable();
    public override Variable<bool> LibVariable => Variable;

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().BoolVariablePrefab;
        var go = Instantiate(canvas);
        go.GetComponent<BoolVariableDisplay>().Variable = this;
        return go;
    }
}
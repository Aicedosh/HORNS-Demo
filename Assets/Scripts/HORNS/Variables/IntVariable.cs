using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IntVariable : DemoVariable<int>
{
    public HORNS.IntegerConsumeVariable Variable { get; } = new HORNS.IntegerConsumeVariable();
    public override Variable<int> LibVariable => Variable;

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().IntVariablePrefab;
        var go = Instantiate(canvas);
        go.GetComponent<IntVariableDisplay>().Variable = this;
        return go;
    }
}
using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IntVariable : DemoVariable<int>
{
    public HORNS.IntVariable Variable { get; } = new HORNS.IntVariable();
    public override Variable<int> LibVariable => Variable;

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().IntVariablePrefab;
        var go = Instantiate(canvas);
        return go;
    }
}
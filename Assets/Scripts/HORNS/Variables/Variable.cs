using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class DemoVariable<T> : DemoVariable, IDisplayable
{
    public abstract Variable<T> LibVariable { get; }
    public override Variable Variable => LibVariable;

    public string Name;

    public abstract LayoutGroup GetComponent();

    public override void AddObserver(IVariableObserver observer)
    {
        LibVariable.Observe(observer);
    }
}
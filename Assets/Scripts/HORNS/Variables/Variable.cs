using HORNS;
using UnityEngine;

public abstract class DemoVariable<T> : DemoVariable, IPrintable
{
    public abstract Variable<T> LibVariable { get; }

    public string Name;

    public string GetText()
    {
        return LibVariable.Value.ToString();
    }

    public string GetName()
    {
        return Name;
    }

    public override void AddObserver(IVariableObserver observer)
    {
        LibVariable.Observe(observer);
    }
}

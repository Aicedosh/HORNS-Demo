using HORNS;
using UnityEngine;

public abstract class DemoVariable<T> : MonoBehaviour, IPrintable
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
}

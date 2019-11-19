using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public abstract class Variable<T> : MonoBehaviour, IPrintable
{
    private HORNS.Variable<T> variable = new HORNS.Variable<T>();
    public HORNS.Variable<T> LibVariable => variable;

    public abstract HORNS.VariableSolver<T> Solver { get; }

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

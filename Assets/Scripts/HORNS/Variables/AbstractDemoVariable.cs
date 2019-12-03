using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DemoVariable : MonoBehaviour
{
    public abstract void AddObserver(HORNS.IVariableObserver observer);
    public abstract HORNS.Variable Variable { get; }
}

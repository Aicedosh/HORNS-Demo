using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolNeed : Need<bool>
{
    public BoolVariable Variable { get; }
    public override Variable<bool> GenericVariable => Variable;

    public float TrueValue;
    public float FalseValue;

    protected override float Evaluate(bool value)
    {
        return value ? TrueValue : FalseValue;
    }

    
}

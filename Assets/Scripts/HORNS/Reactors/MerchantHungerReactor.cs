using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantHungerReactor : VariableReactor<int>
{
    public BoolVariable Works;
    public IntVariable Hunger;

    protected override bool ShouldRecalculate(int value)
    {
        return Works.LibVariable.Value;
    }

    protected override void Start()
    {
        base.Start();
        Hunger.Variable.Observe(this);
    }
}

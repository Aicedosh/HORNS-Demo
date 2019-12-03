using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyReactor : VariableReactor<int>
{
    public IntVariable Money;

    protected override bool ShouldRecalculate(int value)
    {
        return Money.Variable.Value > 10;
    }

    protected override void Start()
    {
        base.Start();
        Money.LibVariable.Observe(this);
    }
}

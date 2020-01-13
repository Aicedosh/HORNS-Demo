using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadishOnFieldReactor : VariableReactor<int>
{
    public IntVariable RadishOnFIeld;
    public BoolVariable HasRadish;
    public BoolVariable IsLonely;

    protected override bool ShouldRecalculate(int value)
    {
        return HasRadish.Variable.Value == false && IsLonely.Variable.Value == false;
    }

    protected override void Start()
    {
        base.Start();
        RadishOnFIeld.LibVariable.Observe(this);
    }
}

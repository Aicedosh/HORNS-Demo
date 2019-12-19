using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantWorkReactor : VariableReactor<bool>
{
    public BoolVariable MerchantWorks;
    public BoolVariable Wood;

    protected override bool ShouldRecalculate(bool value)
    {
        return Wood.Variable.Value;
    }

    protected override void Start()
    {
        base.Start();
        MerchantWorks.Variable.Observe(this);
    }
}

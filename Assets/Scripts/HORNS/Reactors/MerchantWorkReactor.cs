using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantWorkReactor : VariableReactor<bool>
{
    public BoolVariable MerchantWorks;
    public BoolVariable ObjectToSell;

    protected override bool ShouldRecalculate(bool value)
    {
        return ObjectToSell.Variable.Value;
    }

    protected override void Start()
    {
        base.Start();
        MerchantWorks.Variable.Observe(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantWorkReactor : VariableReactor<bool>
{
    [HideInInspector]
    public BoolVariable HasObjectToSell;

    protected override bool ShouldRecalculate(bool value)
    {
        return HasObjectToSell.Variable.Value;
    }

    protected override void Start()
    {
        base.Start();
        GetComponentInParent<ObjectSeller>().Shop.IsOpen.Variable.Observe(this);
    }
}

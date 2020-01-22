using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantHungerReactor : VariableReactor<int>
{
    private BoolVariable isShopOpen;
    private IntVariable hunger;

    protected override bool ShouldRecalculate(int oldValue, int newValue)
    {
        return isShopOpen.LibVariable.Value;
    }

    protected override void Start()
    {
        base.Start();

        hunger = GetComponentInParent<BasicAgent>().Hunger;
        isShopOpen = GetComponentInParent<Merchant>().Shop.IsOpen;

        hunger.Variable.Observe(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class MerchantEatAction : EatAction
{
    private Merchant merchant;

    protected override void Start()
    {
        base.Start();
        merchant = GetComponentInParent<Merchant>();
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(merchant.Shop.IsOpen.Variable, new BooleanPrecondition(false));
    }
}

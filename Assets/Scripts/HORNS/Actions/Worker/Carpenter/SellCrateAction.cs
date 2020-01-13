using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellCrateAction : SellAction
{
    private Transform shopspot;
    private BoolVariable isShopOpen;
    private BoolVariable hasCrate;
    private IntVariable money;
    private Transform merchantSpot;

    public int MoneyGained;

    protected override void Start()
    {
        base.Start();
        ObjectSeller objectSeller = GetComponentInParent<ObjectSeller>();
        shopspot = objectSeller.Shop.ClientSpot;
        isShopOpen = objectSeller.Shop.IsOpen;
        hasCrate = GetComponentInParent<Carpenter>().HasCrate;
        money = GetComponentInParent<Worker>().Money;
        merchantSpot = objectSeller.Shop.MerchantSpot;
    }

    protected override void Perform()
    {
        if (isShopOpen.Variable.Value == false)
        {
            Cancel();
            return;
        }
        basicAgent.GetComponentInChildren<Carrier>().SetAction(this);
        navigator.GoTo(shopspot, OnWalkEnd, merchantSpot);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(isShopOpen.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(hasCrate.Variable, new BooleanPrecondition(true));
        action.AddResult(hasCrate.Variable, new BooleanResult(false));
        action.AddResult(money.Variable, new IntegerAddResult(MoneyGained));
    }
}

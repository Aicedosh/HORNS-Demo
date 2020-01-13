using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellWood : SellAction
{
    private BoolVariable hasWood;
    private IntVariable money;
    private BoolVariable isShopOpen;
    private IntVariable woodCount;

    public int MoneyGained;

    private BasicAgent basicAgent;
    private Transform target;
    private Transform merchantSpot;

    protected override void Start()
    {
        base.Start();
        hasWood = GetComponentInParent<Woodcutter>().HasWood;
        money = GetComponentInParent<Worker>().Money;
        isShopOpen = GetComponentInParent<ObjectSeller>().Shop.IsOpen;
        woodCount = GetComponentInParent<ObjectSeller>().Shop.WoodCount;
        target = GetComponentInParent<ObjectSeller>().Shop.ClientSpot;
        basicAgent = GetComponentInParent<BasicAgent>();
        merchantSpot = GetComponentInParent<ObjectSeller>().Shop.MerchantSpot;
    }

    protected override void Perform()
    {
        if(isShopOpen.Variable.Value == false)
        {
            Cancel();
            return;
        }
        basicAgent.GetComponentInChildren<Carrier>().SetAction(this);
        navigator.GoTo(target, OnWalkEnd, merchantSpot);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(isShopOpen.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(hasWood.Variable, new BooleanPrecondition(true));
        action.AddResult(hasWood.Variable, new BooleanResult(false));
        action.AddResult(money.Variable, new IntegerAddResult(MoneyGained));
        action.AddResult(woodCount.Variable, new IntegerAddResult(1));
    }
}

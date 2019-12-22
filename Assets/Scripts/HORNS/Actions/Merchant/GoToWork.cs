using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class GoToWork : GoToAction
{
    protected override void Perform()
    {
        navigator.GoTo(destination, OnWalkEnd);
    }

    private Transform destination;
    private BoolVariable isShopOpen;

    protected override void Start()
    {
        base.Start();
        destination = GetComponentInParent<Merchant>().Shop.MerchantSpot;
        isShopOpen = GetComponentInParent<Merchant>().Shop.IsOpen;
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(isShopOpen.Variable, new BooleanResult(true));
        action.AddCost(.3f);
    }

}

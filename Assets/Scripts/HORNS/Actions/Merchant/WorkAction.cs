using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class WorkAction : BasicAction
{
    protected virtual void Start()
    {
        isShopOpen = GetComponentInParent<Merchant>().Shop.IsOpen;
    }

    protected override void Perform()
    {
    }

    private BoolVariable isShopOpen;

    public override bool IsIdle => true;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddCost(isShopOpen.Variable, v => v ? 0.9f : 1000); //TODO: DIRTY HACK
        action.AddPrecondition(isShopOpen.Variable, new BooleanPrecondition(true));
    }
}

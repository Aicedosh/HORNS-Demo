using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellCrateAction : GoToAction
{
    private Transform shopspot;
    private BoolVariable isShopOpen;
    private BoolVariable hasCrate;
    private IntVariable money;
    private BasicAgent basicAgent;

    public int MoneyGained;

    protected override void Start()
    {
        base.Start();
        ObjectSeller objectSeller = GetComponentInParent<ObjectSeller>();
        shopspot = objectSeller.Shop.ClientSpot;
        isShopOpen = objectSeller.Shop.IsOpen;
        hasCrate = GetComponentInParent<Carpenter>().HasCrate;
        money = GetComponentInParent<Worker>().Money;
        basicAgent = GetComponentInParent<BasicAgent>();
    }

    protected override void Perform()
    {
        if (isShopOpen.Variable.Value == false)
        {
            Cancel();
            return;
        }
        navigator.GoTo(shopspot, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(isShopOpen.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(hasCrate.Variable, new BooleanPrecondition(true));
        action.AddResult(hasCrate.Variable, new BooleanResult(false));
        action.AddResult(money.Variable, new IntegerAddResult(MoneyGained));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Interact", true);
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Carry", false);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Interact", false);
    }
}

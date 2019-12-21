using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellWood : GoToAction
{
    public BoolVariable Wood;
    public IntVariable Money;
    public BoolVariable SellerWorks;
    public IntVariable WoodCount;

    public int WoodSold;
    public int MoneyGained;

    public Transform target;

    protected override void Perform()
    {
        if(SellerWorks.Variable.Value == false)
        {
            Cancel();
            return;
        }
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(SellerWorks.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(Wood.Variable, new BooleanPrecondition(true));
        action.AddResult(Wood.Variable, new BooleanResult(false));
        action.AddResult(Money.Variable, new IntegerAddResult(MoneyGained));
        action.AddResult(WoodCount.Variable, new IntegerAddResult(1));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Interact", true); //TODO: Make it right
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        GetComponentInChildren<Animator>().SetBool("Carry", false);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        GetComponentInChildren<Animator>().SetBool("Interact", false);
    }
}

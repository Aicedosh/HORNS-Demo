using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellWood : GoToAction
{
    public IntVariable Wood;
    public IntVariable Money;
    public BoolVariable SellerWorks;

    public int WoodSold;
    public int MoneyGained;

    public Transform target;

    protected override void Perform()
    {
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(SellerWorks.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(Wood.Variable, new IntegerPrecondition(WoodSold, IntegerPrecondition.Condition.AtLeast));
        action.AddResult(Wood.Variable, new IntegerAddResult(-WoodSold));
        action.AddResult(Money.Variable, new IntegerAddResult(MoneyGained));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Interact", true); //TODO: Make it right
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        GetComponentInChildren<Animator>().SetBool("Interact", false);
    }
}

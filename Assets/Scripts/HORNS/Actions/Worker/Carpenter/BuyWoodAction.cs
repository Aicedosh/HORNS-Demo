using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class BuyWoodAction : GoToAction
{
    public IntVariable WoodCount;
    public Transform Merchant;
    public BoolVariable MerchantWorks;
    public BoolVariable HasWood;
    public GameObject Log;

    protected override void Perform()
    {
        navigator.GoTo(Merchant, OnWalkEnd);
        GetComponentInChildren<Carrier>().SetCarriedObject(Log);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(WoodCount.Variable, new IntegerPrecondition(1, IntegerPrecondition.Condition.AtLeast));
        action.AddPrecondition(MerchantWorks.Variable, new BooleanPrecondition(true));
        action.AddResult(HasWood.Variable, new BooleanResult(true));
        action.AddResult(WoodCount.Variable, new IntegerAddResult(-1));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Carry", true);
    }
}

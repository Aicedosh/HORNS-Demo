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

    protected override void Perform()
    {
        navigator.GoTo(Merchant, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(WoodCount.Variable, new IntegerPrecondition(1, IntegerPrecondition.Condition.AtLeast));
        action.AddPrecondition(MerchantWorks.Variable, new BooleanPrecondition(true));
        action.AddResult(HasWood.Variable, new BooleanResult(true));
        action.AddResult(WoodCount.Variable, new IntegerAddResult(-1));
    }
}

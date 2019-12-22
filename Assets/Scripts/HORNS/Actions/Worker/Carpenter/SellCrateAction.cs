using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellCrateAction : GoToAction
{
    public Transform Merchant;
    public BoolVariable MerchantWorks;
    public BoolVariable HasCrate;
    public IntVariable Money;

    public int MoneyGained;

    protected override void Perform()
    {
        if (MerchantWorks.Variable.Value == false)
        {
            Cancel();
            return;
        }
        navigator.GoTo(Merchant, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(MerchantWorks.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(HasCrate.Variable, new BooleanPrecondition(true));
        action.AddResult(HasCrate.Variable, new BooleanResult(false));
        action.AddResult(Money.Variable, new IntegerAddResult(MoneyGained));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Interact", true);
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

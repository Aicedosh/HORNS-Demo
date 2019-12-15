using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class EatAction : GoToAction
{
    public IntVariable Hunger;
    public IntVariable Money;
    public BoolVariable Works;

    public int MoneyRequired;
    public int HungerSatisfied;

    public Transform target;

    protected override void Perform()
    {
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        if(Works != null)
        {
            action.AddPrecondition(Works.Variable, new BooleanPrecondition(false));
        }

        if (Money != null)
        {
            action.AddPrecondition(Money.Variable, new IntegerPrecondition(MoneyRequired, IntegerPrecondition.Condition.AtLeast));
            action.AddResult(Money.Variable, new IntegerAddResult(-MoneyRequired));
        }

        action.AddResult(Hunger.Variable, new IntegerAddResult(-HungerSatisfied));
    }
}

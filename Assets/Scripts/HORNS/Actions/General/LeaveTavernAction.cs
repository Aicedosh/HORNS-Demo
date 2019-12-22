using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class LeaveTavernAction : SetBoolAction
{
    private IntVariable numberOfCustomers;

    protected virtual void Start()
    {
        numberOfCustomers = GetComponentInParent<TavernClient>().Tavern.NumberOfCustomers;
        Variable = GetComponentInParent<TavernClient>().IsInTavern;
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(numberOfCustomers.Variable, new IntegerAddResult(-1));
        action.AddCost(Cost);
    }
}

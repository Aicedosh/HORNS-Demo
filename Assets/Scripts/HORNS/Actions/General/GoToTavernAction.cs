using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class GoToTavernAction : GoToAction
{
    private Transform target;
    private IntVariable numberOfCustomers;

    private BoolVariable isInDestTavern;

    protected override void Perform()
    {
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(isInDestTavern.Variable, new BooleanResult(true));
        action.AddResult(numberOfCustomers.Variable, new IntegerAddResult(1));
    }

    protected override void Start()
    {
        base.Start();
        TavernClient tc = GetComponentInParent<TavernClient>();
        target = tc.Tavern.CustomerSpot;
        numberOfCustomers = tc.Tavern.NumberOfCustomers;
        isInDestTavern = tc.IsInTavern;
    }
}

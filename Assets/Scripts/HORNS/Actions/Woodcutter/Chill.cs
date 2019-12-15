using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class Chill : GoToAction
{
    public IntVariable Energy;
    public Transform Home;

    protected override void Perform()
    {
        navigator.GoTo(Home, OnWalkEnd);
    }

    public override bool IsIdle => true;

    protected override void SetupAction(Action action)
    {
        action.AddCost(5);
        action.AddResult(Energy.Variable, new IntegerAddResult(5));
    }
}

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

    protected override void Start()
    {
        base.Start();
        Energy.Variable.Value = 200;
    }

    public override bool IsIdle => false;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddCost(5);
        action.AddResult(Energy.Variable, new IntegerAddResult(5));
    }
}

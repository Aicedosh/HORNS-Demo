using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class CreateAction : GoToAction
{
    public Transform Workshop;
    public BoolVariable Wood;
    public BoolVariable Crate;
    public IntVariable Energy;
    public int EnergyConsumed;

    protected override void Perform()
    {
        navigator.GoTo(Workshop, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(Wood.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(Crate.Variable, new BooleanPrecondition(false));
        action.AddResult(Crate.Variable, new BooleanResult(true));
        action.AddResult(Wood.Variable, new BooleanResult(false));
        action.AddResult(Energy.Variable, new IntegerAddResult(-EnergyConsumed));
    }
}

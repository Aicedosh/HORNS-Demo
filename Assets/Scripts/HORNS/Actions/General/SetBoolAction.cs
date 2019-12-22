using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SetBoolAction : BasicAction
{
    public BoolVariable Variable;
    public bool EndValue;

    public float Cost;

    protected override void Perform()
    {
        Complete();
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(Variable.Variable, new BooleanPrecondition(!EndValue));
        action.AddResult(Variable.Variable, new BooleanResult(EndValue));
        action.AddCost(Cost);
    }

    protected virtual void Start() {}
}

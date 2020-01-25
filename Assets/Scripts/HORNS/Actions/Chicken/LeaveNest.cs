using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class LeaveNest : BasicAction
{
    private Chicken chicken;

    protected override void Start()
    {
        base.Start();
        chicken = GetComponentInParent<Chicken>();
    }

    protected override void Perform()
    {
        Complete();
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(chicken.IsAtNest.Variable, new BooleanResult(false));
    }
}

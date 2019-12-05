using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class Work : BasicAction
{
    protected override void Perform()
    {
    }

    public BoolVariable Works;

    protected override void OnActionEnd(bool success)
    {
        if (success)
        {
            
        }
        else
        {
            base.OnActionEnd(success);
        }
    }

    public override bool IsIdle => true;

    protected override void SetupAction(Action action)
    {
        action.AddCost(Works.Variable, v => v ? 0 : 100);
    }
}

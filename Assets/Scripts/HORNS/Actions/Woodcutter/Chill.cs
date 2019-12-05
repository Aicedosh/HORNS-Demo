using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class Chill : BasicAction
{
    protected override void Perform()
    {

    }

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
        action.AddCost(100);
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class GoToWork : GoToAction
{
    protected override void Perform()
    {
        navigator.GoTo(Destination, OnWalkEnd);
    }

    public Transform Destination;
    public BoolVariable Works;

    protected override void SetupAction(Action action)
    {
        action.AddResult(Works.Variable, new BooleanResult(true));
        action.AddCost(.3f);
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class ChopTree : GoToAction
{
    public Forest Forest;

    public IntVariable Energy;
    public IntVariable Wood;

    public int EnergyLost;
    public int WoodGained;

    private Transform target;

    protected override void SetupAction(Action action)
    {
        action.AddResult(Wood.Variable, new IntegerAddResult(WoodGained));
        action.AddResult(Energy.Variable, new IntegerAddResult(-EnergyLost));
    }

    protected override void Perform()
    {
        target = navigator.GoToNearest(Forest.GetTreesLocations(), Arrived);
    }

    protected override void OnActionEnd(bool success)
    {
        Forest.Remove(target);
        Destroy(target.gameObject);
        base.OnActionEnd(success);
    }
}
using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class ChopTree : BasicAction
{
    public Forest Forest;

    public IntVariable Energy;
    public IntVariable Wood;

    public int EnergyLost;
    public int WoodGained;

    private Transform target;
    private Navigator navigator;

    protected override void SetupAction(Action action)
    {
        action.AddResult(Wood.Variable, new IntegerAddResult(WoodGained));
        action.AddResult(Energy.Variable, new IntegerAddResult(-EnergyLost));
    }

    protected override void Perform()
    {
        target = navigator.GoToNearest(Forest.GetTreesLocations(), OnActionEnd);
    }

    protected override void OnActionEnd(bool success)
    {
        // TODO: PROPER FIX
        if (target == null || target.gameObject == null)
        {
            base.OnActionEnd(false);
        }

        if (success)
        {
            Forest.Remove(target);
            Destroy(target.gameObject);
        }
        base.OnActionEnd(success);
    }

    void Start()
    {
        navigator = GetComponent<Navigator>();
    }
}

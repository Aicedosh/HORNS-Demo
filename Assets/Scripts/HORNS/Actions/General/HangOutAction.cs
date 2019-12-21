using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class HangOutAction : GoToAction
{
    public IntVariable CrowdSize;
    public Transform[] Places;
    public int DesiredCrowdSize;
    public float Factor;

    public BoolVariable Rains;

    public override bool IsIdle => true;

    private bool isAtDest;

    protected override void Perform()
    {
        Transform target = Places[Random.Range(0, Places.Length)];
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);

        if(Rains != null)
        {
            action.AddCost(Rains.Variable, r => r ? 3f : -0.3f);
        }

        action.AddCost(CrowdSize.Variable, n => Factor * (n - DesiredCrowdSize) * (n - DesiredCrowdSize));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        CrowdSize.Variable.Value++;
        isAtDest = true;
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        if(isAtDest)
        {
            CrowdSize.Variable.Value--;
        }
        isAtDest = false;
    }
}

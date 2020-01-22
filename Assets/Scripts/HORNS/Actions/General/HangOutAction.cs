using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;

public class HangOutAction : GoToAction
{
    public int DesiredCrowdSize;
    public float Factor;
    public RestSpot RestSpot;

    public override bool IsIdle => true;

    private Transform[] places;
    private IntVariable crowdSize;
    private bool isAtDest;

    protected override void Start()
    {
        base.Start();
        places = RestSpot.Spots.ToArray();
        crowdSize = RestSpot.CrowdSize;
    }

    protected override void Perform()
    {
        Transform target = places[Random.Range(0, places.Length)];
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);

        action.AddCost(crowdSize.Variable, n => Factor * (n - DesiredCrowdSize) * (n - DesiredCrowdSize));
        action.AddCost(RestSpot.EnemyIsHere.Variable, e => e ? float.PositiveInfinity : 0);
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        crowdSize.Variable.Value++;
        isAtDest = true;
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        if(isAtDest)
        {
            crowdSize.Variable.Value--;
        }
        isAtDest = false;
    }
}

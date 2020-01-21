using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;

public class RunAwayAction : GoToAction
{
    private Transform[] spots;
    private BasicAgent agent;

    protected override void Start()
    {
        base.Start();
        GameObject runAwayGo = GameObject.FindGameObjectWithTag("RunAwaySpot");
        spots = Enumerable.Range(0, runAwayGo.transform.childCount).Select(i => runAwayGo.transform.GetChild(i)).ToArray();
        agent = GetComponentInParent<BasicAgent>();
    }

    protected override void Perform()
    {
        base.Perform();
        navigator.Run();
        navigator.GoToNearest(spots, OnWalkEnd);
        agent.RunsAway = true;
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);

        Merchant m = GetComponentInParent<Merchant>();
        if(m != null)
        {
            action.AddPrecondition(m.Shop.IsOpen.Variable, new BooleanPrecondition(false));
        }

        action.AddPrecondition(agent.IsNearDanger.Variable, new BooleanPrecondition(true));
        action.AddResult(agent.IsNearDanger.Variable, new BooleanResult(false));
    }

    protected override void FinishWork() {
        Cancel();
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        navigator.Walk();
        agent.RunsAway = false;
    }
}

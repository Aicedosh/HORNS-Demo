using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;

public class RunAwayAction : GoToAction
{
    public float RunTime = 10f;
    private float timeElapsed;

    private BasicAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponentInParent<BasicAgent>();
    }

    protected override void Update()
    {
        if(agent.RunsAway)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= RunTime)
            {
                Cancel();
            }
        }
        base.Update();
    }

    protected override void Perform()
    {
        base.Perform();
        navigator.Run();

        navigator.Follow(agent.RunSpot.transform, OnWalkEnd);

        agent.RunsAway = true;
        timeElapsed = 0f;
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

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        navigator.Walk();
        agent.RunsAway = false;
    }
}

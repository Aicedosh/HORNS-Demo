using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoToAction : BasicAction
{
    public float TimeToComplete;
    public bool Hide;
    public bool Show;

    protected Navigator navigator;
    private float timeElapsed;
    private bool started;
    private bool arrived;
    private HideAgent hide;

    private BoolVariable[] isInTavernVariables => GetComponentInParent<BasicAgent>().IsInTavernVariables.ToArray();

    protected override void Perform()
    {
        hide.SetAgentVisibility(true);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);

        foreach (var v in isInTavernVariables)
        {
            action.AddPrecondition(v.Variable, new BooleanPrecondition(false));
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        navigator = GetComponentInParent<Navigator>();
        hide = GetComponentInParent<HideAgent>();

        var ag = GetComponentInParent<BasicAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (arrived)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= TimeToComplete)
            {
                arrived = false;

                FinishWork();
            }
        }
    }

    protected void OnWalkEnd(bool success)
    {
        if (success)
        {
            timeElapsed = 0;
            arrived = true;

            if (Hide)
            {
                hide.SetAgentVisibility(false);
            }

            OnArrive();
        }
        else
        {
            Cancel();
        }
    }

    protected override void OnCancel()
    {
        base.OnCancel();
        navigator.Stop();
        arrived = false;
        if(Hide)
        {
            hide.SetAgentVisibility(true);
        }
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        if (Show)
        {
            hide.SetAgentVisibility(true);
        }
    }

    //Start animation etc.
    protected virtual void OnArrive() { }

    protected virtual void FinishWork() { Complete(); }
}

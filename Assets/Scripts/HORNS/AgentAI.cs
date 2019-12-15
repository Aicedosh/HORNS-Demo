using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    private HORNS.Agent agent = new HORNS.Agent();
    public BasicAction CurrentAction { get; set; }
    public bool PerformedActionThisFrame { get; set; }

    private CancellationTokenSource source;
    private bool shouldRecalculate;
    private bool computing;
    private Task aiTask;

    private string objectName;

    // Start is called before the first frame update
    void Start()
    {
        source = new CancellationTokenSource();
        objectName = name;

        foreach (BasicAction action in gameObject.GetComponents<BasicAction>())
        {
            if (action.IsIdle)
            {
                agent.AddIdleAction(action.CreateAction(this));
            }
            else
            {
                agent.AddAction(action.CreateAction(this));
            }
        }

        foreach (IDemoNeed need in gameObject.GetComponents<IDemoNeed>())
        {
            need.AddTo(agent);
        }

        agent.SetRecalculateCallback((a) =>
        {
            Debug.Log($"Calculated plan for {objectName}: {a.PlannedActions} action{(a.PlannedActions > 1 ? "s" : "")} ({a.LastPlanTime.Milliseconds}ms)");
        });
    }

    private async void Update()
    {
        if (computing == false)
        {
            computing = true; //necessary, as Update will be called again before we finish calculations
            HORNS.Action action = await HandleAI(source.Token);
            action?.Perform();
            computing = false; //This is actually called from the same thread as the original 
        }
    }

    private Task<HORNS.Action> HandleAI(CancellationToken token)
    {
        return Task.Run(async () =>
        {
            bool recalculated = false;
            if (shouldRecalculate)
            {
                await agent.RecalculateActionsAsync(source.Token);
                shouldRecalculate = false;
                recalculated = true;
            }
            if(CurrentAction == null || recalculated)
            {
                return await GetNextActionAsync(token);
            }
            return null;
        });
    }

    private async Task<HORNS.Action> GetNextActionAsync(CancellationToken token)
    {
        try
        {
            HORNS.Action action = await agent.GetNextActionAsync(token);
            return action;
        }
        catch (TaskCanceledException)
        {
            return null;
        }
    }

    public void RecalculatePlan()
    {
        if (source == null)
        {
            //We didn't even start calculations, probably someone called us too early as the reaction to variable value initialization
            return;
        }
        shouldRecalculate = true;
        source.Cancel();
        source = new CancellationTokenSource();
    }

    public void CancelAction()
    {
        CurrentAction?.Cancel();
    }
}

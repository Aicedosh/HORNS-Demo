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

    // Start is called before the first frame update
    void Start()
    {
        source = new CancellationTokenSource();

        foreach (BasicAction action in gameObject.GetComponents<BasicAction>())
        {
            if (action.IsIdle)
            {
                agent.AddIdleAction(action.CreateAction(this));
            }
            else
            {
                agent.AddAction(action.CreateAction(this)); //TODO: Those names are awful... Change to HORNSAction/LibraryAction or something
            }
        }

        foreach (IDemoNeed need in gameObject.GetComponents<IDemoNeed>())
        {
            need.AddTo(agent);
        }
    }

    private async void Update()
    {
        if(CurrentAction == null && computing == false)
        {
            computing = true; //necessary, as Update will be called again before we finish calculations
            HORNS.Action action = await HandleAI(source.Token);
            action?.Perform();
            computing = false; //This is actually called from the same thread as the original call
        }
    }

    private Task<HORNS.Action> HandleAI(CancellationToken token)
    {
        return Task.Run(async () =>
        {
            return await GetNextActionAsync(token);
        });
    }

    private async Task<HORNS.Action> GetNextActionAsync(CancellationToken token)
    {
        try
        {
            if (shouldRecalculate)
            {
                await agent.RecalculateActionsAsync(token);
                shouldRecalculate = false;
            }

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
        shouldRecalculate = true;
        source.Cancel();
        source = new CancellationTokenSource();
    }

    public Transform Home;
    public Transform Tavern;

    public void CancelAction()
    {
        CurrentAction?.Cancel();
    }
}

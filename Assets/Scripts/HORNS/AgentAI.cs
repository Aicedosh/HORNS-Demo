using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    private HORNS.Agent agent = new HORNS.Agent();
    public BasicAction CurrentAction { get; set; }
    public bool PerformedActionThisFrame { get; set; }

    // Start is called before the first frame update
    void Start()
    {
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

    private void Update()
    {
        PerformNextAction();
    }

    public void PerformNextAction()
    {
        if (CurrentAction != null)
        {
            return;
        }
        HORNS.Action action = agent.GetNextAction();
        action?.Perform();
    }

    public void RecalculatePlan()
    {
        Debug.Log("Recalculating...");
        agent.RecalculateActions();
    }

    public Transform Home;
    public Transform Tavern;

    public void CancelAction()
    {
        CurrentAction.Cancel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BasicAction : MonoBehaviour
{
    private class Action : HORNS.Action
    {
        private BasicAction basicAction;
        public Action(BasicAction basicAction)
        {
            this.basicAction = basicAction;
        }

        public override void Perform()
        {
            Debug.Log("Performing action");
            basicAction.agentAI.CurrentAction = basicAction;
            basicAction.Perform();
        }
    }

    private AgentAI agentAI;
    private HORNS.Action action;

    protected abstract void Perform();

    public virtual bool IsIdle => false;


    public HORNS.Action CreateAction(AgentAI agentAI)
    {
        this.agentAI = agentAI;
        action = new Action(this);
        SetupAction(action);

        return action;
    }

    protected virtual void SetupAction(HORNS.Action action) { }

    //Call this method in subclass on action's end (e.g. agent is at the desired target)
    protected virtual void OnActionEnd(bool success)
    {
        Debug.Log($"Action ends with {(success ? "success" : "failure")}");
        agentAI.CurrentAction = null;
        agentAI.PerformedActionThisFrame = true;
        if(success)
        {
            action.Apply();
        }
        else
        {
            agentAI.RecalculatePlan();
        }
        agentAI.PerformedActionThisFrame = false;
    }

    public void Cancel()
    {
        Debug.Log("Cancelled action");
        OnActionEnd(false);
    }
}

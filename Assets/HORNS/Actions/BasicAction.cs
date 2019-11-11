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

        protected override void ActionResult()
        {
            basicAction.ActionResult();
        }
    }

    private AgentAI agentAI;

    protected abstract void ActionResult();

    public HORNS.Action CreateAction(AgentAI agentAI)
    {
        this.agentAI = agentAI;
        Action action = new Action(this);

        //TODO: add precondtions, effects and cost

        return action;
    }

    //Call this method in subclass on action's end (e.g. agent is at the desired target)
    protected void OnActionEnd()
    {
        agentAI.IsExecuting = false;
    }
}

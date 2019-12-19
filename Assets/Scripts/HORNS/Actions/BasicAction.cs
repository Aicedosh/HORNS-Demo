using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BasicAction : MonoBehaviour
{
    public class LibAction : HORNS.Action
    {
        private BasicAction basicAction;
        public LibAction(BasicAction basicAction)
        {
            this.basicAction = basicAction;
        }

        public string Name => basicAction.GetType().Name;

        public override void Perform()
        {
            if (basicAction.agentAI.CurrentAction == basicAction)
            {
                //Newly calculated action is the one we are performing, do nothing
                return;
            }

            Debug.Log($"[{basicAction.agentAI.gameObject.name}] Performing action: {basicAction.GetType().Name}");
            if(basicAction.agentAI.CurrentAction != null)
            {
                Debug.Log($"[{basicAction.agentAI.gameObject.name}] Cancelling previous action: {basicAction.agentAI.CurrentAction.GetType().Name}");
                basicAction.agentAI.CurrentAction.OnActionEnd();
                basicAction.agentAI.CurrentAction.OnCancel();
            }

            basicAction.agentAI.CurrentAction = basicAction;
            basicAction.Perform();
        }
    }

    public HORNS.Action CreateAction(AgentAI agentAI)
    {
        this.agentAI = agentAI;
        action = new LibAction(this);
        SetupAction(action);

        return action;
    }

    protected AgentAI agentAI;
    private HORNS.Action action;

    protected abstract void Perform();

    public virtual bool IsIdle => false;

    protected virtual void SetupAction(HORNS.Action action) { }

    //Override this method to specify the uninterruptible part of action
    protected virtual void OnComplete() { }

    protected virtual void OnCancel() { }
    protected virtual void OnActionEnd() { }

    //Call this method in subclass to begin the uninterruptible part of action
    protected void Complete()
    {
        Debug.Log($"[{agentAI.gameObject.name}] Completed action {GetType().Name}");
        EndAction(true);
    }

    public void Cancel()
    {
        Debug.Log($"[{agentAI.gameObject.name}] Cancelled action {GetType().Name}");
        EndAction(false);
    }

    protected virtual void EndAction(bool success)
    {
        Debug.Log($"[{agentAI.gameObject.name}] Action ends with {(success ? "success" : "failure")}");
        OnActionEnd();
        agentAI.CurrentAction = null;
        agentAI.PerformedActionThisFrame = true;

        if(success && !action.CanExecute)
        {
            int i = 0;
        }

        if (success)
        {
            if(!action.Apply())
            {
                int i = 0;
            }
            OnComplete();
        }
        else
        {
            agentAI.RecalculatePlan();
            OnCancel();
        }
        agentAI.PerformedActionThisFrame = false;
    }
}

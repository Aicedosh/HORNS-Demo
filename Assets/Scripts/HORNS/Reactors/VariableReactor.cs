using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VariableReactor<T> : MonoBehaviour, HORNS.IVariableObserver<T>
{
    protected AgentAI agentAI;

    protected abstract bool ShouldRecalculate(T value);
    public void ValueChanged(T value)
    {
        if(ShouldRecalculate(value) && !agentAI.PerformedActionThisFrame)
        {
            agentAI.CancelAction();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agentAI = GetComponent<AgentAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
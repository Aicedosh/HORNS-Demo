using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainReactor : MonoBehaviour, HORNS.IVariableObserver
{
    private AgentAI agentAI;

    private void Start()
    {
        FindObjectOfType<Weather>().Rains.Variable.Observe(this);
        agentAI = GetComponentInParent<AgentAI>();
    }

    public void ValueChanged()
    {
        agentAI.RecalculatePlan();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainReactor : MonoBehaviour, HORNS.IVariableObserver<bool>
{
    private AgentAI agentAI;

    private void Start()
    {
        FindObjectOfType<Weather>().Rains.Variable.Observe(this);
        agentAI = GetComponentInParent<AgentAI>();
    }

    public void ValueChanged(bool old, bool value)
    {
        agentAI.RecalculatePlan();
        Navigator nav = agentAI.GetComponent<Navigator>();
        if(value)
        {
            nav.Run();
        }
        else
        {
            nav.Walk();
        }
    }
}

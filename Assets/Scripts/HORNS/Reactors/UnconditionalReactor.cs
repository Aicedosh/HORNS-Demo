using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnconditionalReactor : MonoBehaviour, HORNS.IVariableObserver
{
    public DemoVariable[] Variables;

    private AgentAI agentAI;

    public void ValueChanged()
    {
        if(!agentAI.PerformedActionThisFrame)
        {
            agentAI.RecalculatePlan();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agentAI = GetComponent<AgentAI>();
        foreach(DemoVariable var in Variables)
        {
            var.AbstractVariable.Observe(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

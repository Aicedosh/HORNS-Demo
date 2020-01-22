using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerReactor : VariableReactor<bool>
{
    private BasicAgent agent;
    protected override void Start()
    {
        base.Start();
        agent = GetComponentInParent<BasicAgent>();
        agent.IsNearDanger.Variable.Observe(this);
    }

    protected override bool ShouldRecalculate(bool oldValue, bool newValue)
    {
        Debug.Log($"[{transform.parent.gameObject.name}] {oldValue} : {newValue} ; RA: {agent.RunsAway}");
        return newValue && !agent.RunsAway && oldValue != newValue;
    }
}

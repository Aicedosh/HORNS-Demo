using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableAgent : Clickable
{
    private Canvas agentCanvas;
    private AgentUI agentUI;

    public override void OnClick(Vector3 position)
    {
        AgentAI agentAI = GetComponentInParent<AgentAI>();
        if (agentAI != null)
        {
            agentCanvas.enabled = true;
            agentUI.SelectAgent(agentAI);
        }
    }

    private void Start()
    {
        agentUI = FindObjectOfType<AgentUI>();
        agentCanvas = agentUI.GetComponent<Canvas>();
    }
}

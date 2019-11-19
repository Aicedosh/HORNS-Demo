using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableAgent : Clickable
{

    public Canvas AgentCanvasGroup;
    private AgentUI agentUI;

    public override void OnClick(Vector3 position)
    {
        AgentAI agentAI = GetComponentInParent<AgentAI>();
        if (agentAI != null)
        {
            AgentCanvasGroup.gameObject.SetActive(true);
            agentUI.SelectAgent(agentAI);
        }
    }

    private void Start()
    {
        agentUI = AgentCanvasGroup.GetComponent<AgentUI>();
    }
}

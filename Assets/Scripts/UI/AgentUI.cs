using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentUI : MonoBehaviour
{
    private AgentAI selectedAgent;
    public Text Text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(selectedAgent == null)
        {
            return;
        }
        string variableString = "";
        foreach (var printable in selectedAgent.GetComponentsInChildren<IPrintable>())
        {
            variableString += printable.GetName() + ": " + printable.GetText() + "\n";
        }

        Text.text = variableString;
    }

    public void PerformAction()
    {
        selectedAgent?.PerformNextAction();
    }

    public void SelectAgent(AgentAI agent)
    {
        selectedAgent = agent;
    }
}

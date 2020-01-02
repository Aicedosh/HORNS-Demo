using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentUI : MonoBehaviour
{
    private AgentAI selectedAgent;
    public LayoutGroup DisplayCanvas;

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
    }

    public void SelectAgent(AgentAI agent)
    {
        if (selectedAgent == agent)
            return;

        selectedAgent = agent;

        while(DisplayCanvas.transform.childCount > 0)
        {
            Transform child = DisplayCanvas.transform.GetChild(0);
            child.SetParent(null); //Become Batman!
            Destroy(child.gameObject);
        }

        foreach (var outline in FindObjectsOfType<Outline>())
        {
            outline.enabled = false;
        }

        if (selectedAgent != null)
        {
            foreach (var displayable in selectedAgent.GetComponentsInChildren<IDisplayable>())
            {
                var go = displayable.GetComponent();
                go.transform.SetParent(DisplayCanvas.transform);
            }

            Outline outline = selectedAgent.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }
        }
    }
}

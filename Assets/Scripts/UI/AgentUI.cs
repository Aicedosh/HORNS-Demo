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

        if (selectedAgent != null)
        {
            var oc = selectedAgent.GetComponent<OutlineController>();
            if (oc != null)
            {
                oc.Selected = false;
            }

            var hc = selectedAgent.GetComponent<Worker>()?.Home?.GetComponent<OutlineController>();
            if (hc != null)
            {
                hc.Selected = false;
            }
        }

        selectedAgent = agent;

        while (DisplayCanvas.transform.childCount > 0)
        {
            Transform child = DisplayCanvas.transform.GetChild(0);
            child.SetParent(null); //Become Batman!
            Destroy(child.gameObject);
        }

        if (selectedAgent != null)
        {
            foreach (var displayable in selectedAgent.GetComponentsInChildren<IDisplayable>())
            {
                var go = displayable.GetComponent();
                go.transform.SetParent(DisplayCanvas.transform);
            }

            var oc = selectedAgent.GetComponent<OutlineController>();
            if (oc != null)
            {
                oc.Selected = true;
            }

            var hc = selectedAgent.GetComponent<Worker>()?.Home?.GetComponent<OutlineController>();
            if (hc != null)
            {
                hc.Selected = true;
            }
        }
    }

    public Transform GetSelectedAgent()
    {
        if (selectedAgent == null)
        {
            return null;
        }
        return selectedAgent.GetComponent<Transform>();
    }
}

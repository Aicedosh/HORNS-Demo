using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    private HORNS.Agent agent = new HORNS.Agent();
    public bool IsExecuting { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        foreach(BasicAction action in gameObject.GetComponents<BasicAction>())
        {
            agent.AddAction(action.CreateAction(this)); //TODO: Those names are awful... Change to HORNSAction/LibraryAction or something
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsExecuting)
        {
            IsExecuting = true; //TODO: I don't really like this (change flag to something smarter...)
            agent.GetNextAction().Perform();
        }
    }
}

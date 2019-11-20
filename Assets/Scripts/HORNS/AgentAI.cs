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
            if(action.IsIdle)
            {
                agent.AddIdleAction(action.CreateAction(this));
            }
            else
            {
                agent.AddAction(action.CreateAction(this)); //TODO: Those names are awful... Change to HORNSAction/LibraryAction or something
            }
        }

        foreach(INeed need in gameObject.GetComponents<INeed>())
        {
            need.AddTo(agent);
        }
        IsExecuting = false;
    }

    private void Update()
    {
        PerformNextAction();
    }

    public void PerformNextAction()
    {
        if(IsExecuting)
        {
            return;
        }
        HORNS.Action action = agent.GetNextAction();
        action?.Perform();
    }

    public Transform Home;
    public Transform Tavern;

    public void GoHome()
    {
        GetComponent<Navigator>().GoTo(Home);
    }

    public void GoTavern()
    {
        GetComponent<Navigator>().GoTo(Tavern);
    }
}

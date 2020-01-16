using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAgents : MonoBehaviour
{
    private bool init;
    private bool skip;
    private void Update()
    {
        //This is necessary because Unity can't even load scene in editor properly
        if (!init)
        {
            if(skip)
            {
                foreach (AgentAI a in FindObjectsOfType<AgentAI>())
                {
                    a.Init();
                    a.InitializedActions = true;
                }

                init = true;
            }
            skip = true;
        }
    }
}

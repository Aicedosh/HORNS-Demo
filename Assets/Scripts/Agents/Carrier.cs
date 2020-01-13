using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    private GameObject carried = null;
    private BasicAction action = null;

    private AgentAI agentAI;

    private void Start()
    {
        agentAI = GetComponentInParent<AgentAI>();
    }

    public void SetCarriedObject(GameObject go)
    {
        carried = go;
    }

    public void SetAction(BasicAction action)
    {
        this.action = action;
    }

    public void PickupEvent()
    {
        carried.SetActive(true);

        action.OnPickup();
    }

    public void PutdownEvent()
    {
        carried.SetActive(false);

        action.OnPutdown();
    }
}

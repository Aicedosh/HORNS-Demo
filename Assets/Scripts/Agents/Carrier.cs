using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    private GameObject carried = null;

    private AgentAI agentAI;

    private void Start()
    {
        agentAI = GetComponentInParent<AgentAI>();
    }

    public void SetCarriedObject(GameObject go)
    {
        carried = go;
    }

    public void PickupEvent()
    {
        carried.SetActive(true);

        agentAI.CurrentAction?.OnPickup();
    }

    public void PutdownEvent()
    {
        carried.SetActive(false);

        agentAI.CurrentAction?.OnPutdown();
    }
}

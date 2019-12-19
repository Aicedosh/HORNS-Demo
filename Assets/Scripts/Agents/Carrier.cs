using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    private GameObject carried = null;

    public void SetCarriedObject(GameObject go)
    {
        carried = go;
    }

    public void PickupEvent()
    {
        carried.SetActive(true);
    }

    public void PutdownEvent()
    {
        carried.SetActive(false);
    }
}

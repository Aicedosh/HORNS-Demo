using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = transform.Find("Actions").gameObject;

        foreach(RestSpot rs in FindObjectsOfType<RestSpot>())
        {
            Eat c = go.AddComponent<Eat>();
            c.RestSpot = rs;
            c.CrowdFactor = 1;
            c.TimeToComplete = 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

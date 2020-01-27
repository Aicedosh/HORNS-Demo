using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public IntVariable Energy;
    public BoolVariable IsAtNest;
    public BoolVariable IsAngry;

    public int ScareBaseCost;
    public int ScareEnergyCost;
    public int ScareCrowdFactor;
    public int ScareTime;
    public int StartEnergy;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = transform.Find("Actions").gameObject;

        foreach(RestSpot rs in FindObjectsOfType<RestSpot>())
        {
            Scare c = go.AddComponent<Scare>();
            c.RestSpot = rs;
            c.CrowdFactor = ScareCrowdFactor;
            c.TimeToComplete = ScareTime;
            c.ActionName = "Scare";
            c.EnergyCost = ScareEnergyCost;
            c.BaseCost = ScareBaseCost;
        }

        Energy.Variable.Value = StartEnergy;
        IsAngry.Variable.Value = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

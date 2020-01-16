using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    public BoolVariable HasWood;
    public BoolVariable HasCrate;
    public Transform Spot;

    public GameObject WoodGo;
    public GameObject CrateGo;

    public bool Occupied { get; set; }

    private void Start()
    {
        SetObject(WorkshopObject.None);
    }

    public enum WorkshopObject
    {
        None, Wood, Crate
    }

    public void SetObject(WorkshopObject obj)
    {
        switch (obj)
        {
            case WorkshopObject.None:
                WoodGo.SetActive(false);
                CrateGo.SetActive(false);
                break;
            case WorkshopObject.Wood:
                WoodGo.SetActive(true);
                CrateGo.SetActive(false);
                break;
            case WorkshopObject.Crate:
                WoodGo.SetActive(false);
                CrateGo.SetActive(true);
                break;
            default:
                throw new System.NotSupportedException("Chosen object is not supported");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSeller : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<MerchantWorkReactor>().HasObjectToSell = GetComponentInParent<Woodcutter>().HasWood;
    }
}

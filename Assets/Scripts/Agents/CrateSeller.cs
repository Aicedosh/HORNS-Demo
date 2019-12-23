using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSeller : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<MerchantWorkReactor>().HasObjectToSell = GetComponentInParent<Carpenter>().HasCrate;
    }
}

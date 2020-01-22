using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Transform MerchantSpot;
    public Transform ClientSpot;
    public BoolVariable IsOpen;
    public IntVariable WoodCount;
    public GameObject ClosedSign;

    public bool Occupied { get; set; }

    public GameObject[] WoodGos;

    private void Update()
    {
        for (int i = 0; i < WoodGos.Length; i++)
        {
            WoodGos[i].SetActive(i + 1 <= WoodCount.Variable.Value);
        }

        ClosedSign.SetActive(!IsOpen.Variable.Value);
    }
}

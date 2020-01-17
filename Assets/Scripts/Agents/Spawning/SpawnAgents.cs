using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnAgents : MonoBehaviour
{
    public class CountParams
    {
        public int MerchantCount;
        public int WoodcutterCount;
        public int CarpenterCount;
        public int FarmerCount;
    }

    private static CountParams instance;
    public static CountParams Params
    {
        get
        {
            if(instance == null)
            {
                instance = new CountParams
                {
                    MerchantCount = 3,
                    WoodcutterCount = 9,
                    CarpenterCount = 5,
                    FarmerCount = 12
                };
            }
            return instance;
        }
    }

    private void Start()
    {
        transform.Find("Merchants").GetComponent<AgentSpawner>().Spawn(Params.MerchantCount);
        transform.Find("Woodcutters").GetComponent<AgentSpawner>().Spawn(Params.WoodcutterCount);
        transform.Find("Carpenters").GetComponent<AgentSpawner>().Spawn(Params.CarpenterCount);
        transform.Find("Farmers").GetComponent<AgentSpawner>().Spawn(Params.FarmerCount);

        foreach(Shop s in FindObjectsOfType<Shop>().Where(s => s.Occupied == false))
        {
            Destroy(s.gameObject);
        }
    }
}

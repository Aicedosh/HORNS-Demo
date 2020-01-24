using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnAgents : MonoBehaviour
{
    private int quitAfter = -1;
    private float startTime;

    public GameObject ChickenPrefab;
    public Vector3 ChickenSpawnLocation;

    public class CountParams
    {
        public int MerchantCount;
        public int WoodcutterCount;
        public int CarpenterCount;
        public int FarmerCount;

        public bool Chicken;
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
                    FarmerCount = 12,

                    Chicken = true
                };
            }
            return instance;
        }
    }

    private void Spawn(string name, int count)
    {
        transform.Find($"{name}s").GetComponent<AgentSpawner>().Spawn(count);

        if(CommandLineParser.LogTimes)
        {
            transform.Find($"{name}s").GetChild(0).GetComponent<AgentAI>().EnableTimeLog($"{name}-{DateTime.Now.ToString("hh-mm-ss")}.txt");
        }
    }

    private void Update()
    {
        if(quitAfter != -1 && (Time.time - startTime) >= quitAfter)
        {
            Application.Quit();
        }
    }

    private void Start()
    {
        startTime = Time.time;
        if(CommandLineParser.QuitAfter.HasValue)
        {
            quitAfter = CommandLineParser.QuitAfter.Value;
        }

        Spawn("Merchant", CommandLineParser.Merchants.HasValue ? CommandLineParser.Merchants.Value : Params.MerchantCount);
        Spawn("Woodcutter", CommandLineParser.Woodcutters.HasValue ? CommandLineParser.Woodcutters.Value : Params.WoodcutterCount);
        Spawn("Carpenter", CommandLineParser.Carpenters.HasValue ? CommandLineParser.Carpenters.Value : Params.CarpenterCount);
        Spawn("Farmer", CommandLineParser.Farmers.HasValue ? CommandLineParser.Farmers.Value : Params.FarmerCount);

        foreach(Shop s in FindObjectsOfType<Shop>().Where(s => s.Occupied == false))
        {
            Destroy(s.gameObject);
        }

        if(Params.Chicken && !CommandLineParser.DisableChicken)
        {
            Instantiate(ChickenPrefab, ChickenSpawnLocation, Quaternion.identity);
        }
    }
}

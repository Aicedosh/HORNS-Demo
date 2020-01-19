using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Worker : MonoBehaviour, IAgentConfigure
{
    private const string RAIN_TAG = "RainRestSpot";
    public Home Home;
    public IntVariable Energy;
    public IntVariable Money;

    public GameObject TavernClientPrefab;

    public int MinTaverns;
    public int MaxTaverns;

    public int MinRestSpots;
    public int MaxRestSpots;

    public void Configure(Home home)
    {
        Home = home;

        GameObject actions = transform.Find("Actions").gameObject;
        int numRestSpots = Random.Range(MinRestSpots, MaxRestSpots + 1);
        foreach (RestSpot rs in home.GetClosest<RestSpot>(numRestSpots, s => s.tag != RAIN_TAG))
        {
            HangOutAction a = actions.AddComponent<HangOutAction>();
            //TODO: Parametrize and randomize
            a.RestSpot = rs;
            a.RainCost = 10;
            a.BaseCost = -1;
            a.TimeToComplete = 30;
            a.DesiredCrowdSize = 2;
            a.Factor = 0.5f;
        }


        HangOutAction rainRest = actions.AddComponent<HangOutAction>();
        //TODO: Parametrize and randomize
        rainRest.RestSpot = home.GetClosest<RestSpot>(s => s.tag == RAIN_TAG);
        rainRest.RainCost = -3;
        rainRest.BaseCost = 3;
        rainRest.TimeToComplete = 10;
        rainRest.DesiredCrowdSize = 2;
        rainRest.Factor = 0.5f;

        MixinConfigure mixinConfigure = new MixinConfigure(transform, home);
        mixinConfigure.Add<TavernClient, Tavern>(TavernClientPrefab, MinTaverns, MaxTaverns, (c, t) => c.Tavern = t);
    }

    private void OnMouseEnter()
    {
        Home.GetComponent<OutlineController>().Hovered = true;
    }

    private void OnMouseExit()
    {
        Home.GetComponent<OutlineController>().Hovered = false;
    }
}

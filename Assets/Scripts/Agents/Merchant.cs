using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, IAgentConfigure
{
    public int MinTaverns;
    public int MaxTaverns;

    public GameObject TavernClientPrefab;

    public Shop Shop;

    public void Configure(Home home)
    {
        Shop = home.GetClosest<Shop>(s => !s.Occupied);
        Shop.Occupied = true;

        MixinConfigure mixinConfigure = new MixinConfigure(transform, home);
        mixinConfigure.Add<TavernClient, Tavern>(TavernClientPrefab, MinTaverns, MaxTaverns, (c, t) => c.Tavern = t);
    }
}

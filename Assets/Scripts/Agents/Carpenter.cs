using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpenter : MonoBehaviour, IAgentConfigure
{
    public Workshop Workshop;

    public BoolVariable HasWood;
    public BoolVariable HasCrate;

    public GameObject Log;
    public GameObject Crate;
    public GameObject Hammer;

    public GameObject CrateSellerPrefab;
    public int MinShops;
    public int MaxShops;

    public void Configure(Home home)
    {
        Workshop = home.GetClosest<Workshop>(w => !w.Occupied);
        Workshop.Occupied = true;

        MixinConfigure mixinConfigure = new MixinConfigure(transform, home);
        mixinConfigure.Add<ObjectSeller, Shop>(CrateSellerPrefab, MinShops, MaxShops, (c, t) => c.Shop = t, s => s.Occupied);
    }
}

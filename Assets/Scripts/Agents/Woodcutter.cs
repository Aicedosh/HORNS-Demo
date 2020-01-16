using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : MonoBehaviour, HORNS.IVariableObserver<bool>, IAgentConfigure
{
    public BoolVariable HasWood;
    public GameObject HandAxe;
    public GameObject HipAxe;
    public GameObject Log;

    public GameObject WoodSellerPrefab;
    public int MinShops;
    public int MaxShops;

    public Forest Forest;

    private Animator _anim;

    public void Configure(Home home)
    {
        Forest = home.GetClosest<Forest>();

        MixinConfigure mixinConfigure = new MixinConfigure(transform, home);
        mixinConfigure.Add<ObjectSeller, Shop>(WoodSellerPrefab, MinShops, MaxShops, (c, t) => c.Shop = t);
    }

    public void ValueChanged(bool value)
    {
        if(value)
        {
            _anim.SetBool("Carry", true);
        }
    }

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        HasWood.Variable.Observe(this);
        Log.SetActive(false);
        HandAxe.SetActive(false);
    }

    void Update()
    {
        if(_anim.GetBool("Chop"))
        {
            HandAxe.SetActive(true);
            HipAxe.SetActive(false);
        }
        else
        {
            HandAxe.SetActive(false);
            HipAxe.SetActive(true);
        }
    }
}

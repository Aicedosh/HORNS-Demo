using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour, IAgentConfigure
{
    public RadishField RadishField;
    public Tavern Tavern;

    public BoolVariable HasRadish;
    public GameObject Radish;

    public void Configure(Home home)
    {
        RadishField = home.GetClosest<RadishField>();
        Tavern = home.GetClosest<Tavern>();
    }

    private void Update()
    {
        Radish.SetActive(HasRadish.Variable.Value);
    }
}

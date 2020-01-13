using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    public RadishField RadishField;
    public Tavern Tavern;

    public BoolVariable HasRadish;
    public GameObject Radish;

    private void Update()
    {
        Radish.SetActive(HasRadish.Variable.Value);
    }
}

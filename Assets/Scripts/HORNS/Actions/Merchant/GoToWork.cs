using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class GoToWork : BasicAction
{
    protected override void Perform()
    {
        navigator.GoTo(Destination, OnActionEnd);
    }

    public Transform Destination;
    public BoolVariable Works;

    protected override void SetupAction(Action action)
    {
        action.AddResult(Works.Variable, new BooleanResult(true));
        action.AddCost(30);
    }

    private Navigator navigator;

    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<Navigator>();
    }

}

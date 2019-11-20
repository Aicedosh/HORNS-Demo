using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class RestAction : BasicAction
{
    public Transform RestSpot;
    private Navigator navigator;

    protected override void ActionResult()
    {
        navigator.GoTo(RestSpot, OnActionEnd);
    }

    public override bool IsIdle => true;

    protected override void SetupAction(Action action)
    {
        action.AddCost(100); //will perform not so willingly
    }

    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<Navigator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

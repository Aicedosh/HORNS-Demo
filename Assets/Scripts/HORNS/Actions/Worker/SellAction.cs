using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SellAction : GoToAction
{
    private BasicAgent basicAgent;

    protected override void Start()
    {
        base.Start();
        basicAgent = GetComponentInParent<BasicAgent>();
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Interact", true);
        basicAgent.GetComponentInChildren<Animator>().SetBool("Carry", false);
    }

    protected override void OnCancel()
    {
        base.OnCancel();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Carry", true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Interact", false);
    }
}

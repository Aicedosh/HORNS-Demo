using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;

public class Eat : GoToAction
{
    public RestSpot RestSpot;
    public int CrowdFactor;

    public override bool IsIdle => true;

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInParent<BasicAgent>().GetComponentInChildren<Animator>();
    }

    protected override void Perform()
    {
        navigator.GoTo(RestSpot.Spots.OrderBy(t => System.Guid.NewGuid()).First().transform, OnWalkEnd);
        animator.SetBool("Run", true);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddCost(RestSpot.CrowdSize.Variable, v => -CrowdFactor * v);
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        animator.SetBool("Run", false);
        animator.SetBool("Eat", true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        animator.SetBool("Eat", false);
    }
}

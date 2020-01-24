using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;

public class Scare : GoToAction
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
        action.AddCost(RestSpot.EnemyIsHere.Variable, e => e ? 1f : 0f);
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        animator.SetBool("Run", false);
        animator.SetBool("Turn Head", true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        animator.SetBool("Turn Head", false);
    }

    protected override void Update()
    {
        base.Update();
        RestSpot.EnemyIsHere.Variable.Value = (transform.position - RestSpot.transform.position).magnitude <= 5f;
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class PickRadish : GoToAction
{
    private Farmer farmer;
    private Animator animator;
    private Worker worker;

    public int EnergyLost;

    protected override void Start()
    {
        base.Start();
        farmer = GetComponentInParent<Farmer>();
        animator = farmer.GetComponentInChildren<Animator>();
        worker = GetComponentInParent<Worker>();
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(farmer.HasRadish.Variable, new BooleanPrecondition(false));
        action.AddResult(farmer.HasRadish.Variable, new BooleanResult(true));
        action.AddResult(worker.Energy.Variable, new IntegerAddResult(-EnergyLost));
    }

    protected override void Perform()
    {
        navigator.GoToNearest(farmer.RadishField.Spots, OnWalkEnd);
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        animator.SetBool("Gather", true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        animator.SetBool("Gather", false);
    }
}

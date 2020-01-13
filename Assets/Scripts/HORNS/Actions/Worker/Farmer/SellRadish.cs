using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class SellRadish : GoToAction
{
    private Farmer farmer;
    private Animator animator;
    private Worker worker;

    public int MoneyGained;

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
        action.AddPrecondition(farmer.HasRadish.Variable, new BooleanPrecondition(true));
        action.AddResult(farmer.HasRadish.Variable, new BooleanResult(false));
        action.AddResult(worker.Money.Variable, new IntegerAddResult(MoneyGained));
    }

    protected override void Perform()
    {
        navigator.GoTo(farmer.Tavern.KitchenSpot, OnWalkEnd);
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        animator.SetBool("Interact", true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        animator.SetBool("Interact", false);
    }
}

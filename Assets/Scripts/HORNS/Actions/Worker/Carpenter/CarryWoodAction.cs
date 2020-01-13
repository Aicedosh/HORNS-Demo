using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class CarryWoodAction : GoToAction
{
    private BoolVariable hasWood;
    private BoolVariable workshopHasWood;
    private Transform workspot;
    private Carpenter carpenter;

    private BasicAgent basicAgent;

    protected override void Start()
    {
        base.Start();
        carpenter = GetComponentInParent<Carpenter>();
        hasWood = carpenter.HasWood;
        workshopHasWood = carpenter.Workshop.HasWood;
        workspot = carpenter.Workshop.Spot;

        basicAgent = GetComponentInParent<BasicAgent>();
    }

    protected override void Perform()
    {
        basicAgent.GetComponentInChildren<Carrier>().SetAction(this);
        navigator.GoTo(workspot, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(hasWood.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(workshopHasWood.Variable, new BooleanPrecondition(false));
        action.AddResult(workshopHasWood.Variable, new BooleanResult(true));
        action.AddResult(hasWood.Variable, new BooleanResult(false));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Carry", false);
    }

    public override void OnPutdown()
    {
        base.OnPutdown();
        carpenter.Workshop.SetObject(Workshop.WorkshopObject.Wood);
    }
}

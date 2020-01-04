using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class PickupCrateAction : GoToAction
{
    private Transform workspot;
    private BoolVariable workshopHasCrate;
    private BoolVariable carpenterHasCrate;

    private GameObject crateGo;
    private Carpenter carpenter;

    protected override void Start()
    {
        base.Start();
        carpenter = GetComponentInParent<Carpenter>();
        workspot = carpenter.Workshop.Spot;
        workshopHasCrate = carpenter.Workshop.HasCrate;
        carpenterHasCrate = carpenter.HasCrate;
        crateGo = carpenter.Crate;
    }

    protected override void Perform()
    {
        navigator.GoTo(workspot, OnWalkEnd);
        carpenter.GetComponentInChildren<Carrier>().SetCarriedObject(crateGo);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(workshopHasCrate.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(carpenterHasCrate.Variable, new BooleanPrecondition(false));
        action.AddResult(carpenterHasCrate.Variable, new BooleanResult(true));
        action.AddResult(workshopHasCrate.Variable, new BooleanResult(false));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        carpenter.GetComponentInChildren<Animator>().SetBool("Carry", true);
    }

    public override void OnPickup()
    {
        base.OnPickup();
        carpenter.Workshop.SetObject(Workshop.WorkshopObject.None);
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class PickupCrateAction : GoToAction
{
    public Transform Workshop;
    public BoolVariable Crate;
    public BoolVariable HasCrate;

    public GameObject CrateGo;

    protected override void Perform()
    {
        navigator.GoTo(Workshop, OnWalkEnd);
        GetComponentInChildren<Carrier>().SetCarriedObject(CrateGo);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(Crate.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(HasCrate.Variable, new BooleanPrecondition(false));
        action.AddResult(HasCrate.Variable, new BooleanResult(true));
        action.AddResult(Crate.Variable, new BooleanResult(false));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Carry", true);
    }
}

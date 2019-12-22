using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class CreateAction : GoToAction
{
    public Transform Workshop;
    public BoolVariable Wood;
    public BoolVariable Crate;
    public IntVariable Energy;
    public int EnergyConsumed;

    public GameObject Hammer;

    protected override void Perform()
    {
        navigator.GoTo(Workshop, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(Wood.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(Crate.Variable, new BooleanPrecondition(false));
        action.AddResult(Crate.Variable, new BooleanResult(true));
        action.AddResult(Wood.Variable, new BooleanResult(false));
        action.AddResult(Energy.Variable, new IntegerAddResult(-EnergyConsumed));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Craft", true);
        Hammer.SetActive(true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        GetComponentInChildren<Animator>().SetBool("Craft", false);
        Hammer.SetActive(false);
    }
}

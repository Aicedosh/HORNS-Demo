using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class CreateAction : GoToAction
{
    private Transform workspot;
    private BoolVariable workshopHasWood;
    private BoolVariable workshopHasCrate;
    private IntVariable energy;
    public int EnergyConsumed;

    private GameObject hammer;
    private Carpenter carpenter;

    protected override void Start()
    {
        base.Start();
        carpenter = GetComponentInParent<Carpenter>();

        workspot = carpenter.Workshop.Spot;
        workshopHasWood = carpenter.Workshop.HasWood;
        workshopHasCrate = carpenter.Workshop.HasCrate;
        energy = GetComponentInParent<Worker>().Energy;
        hammer = carpenter.Hammer;
    }

    protected override void Perform()
    {
        navigator.GoTo(workspot, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(workshopHasWood.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(workshopHasCrate.Variable, new BooleanPrecondition(false));
        action.AddResult(workshopHasCrate.Variable, new BooleanResult(true));
        action.AddResult(workshopHasWood.Variable, new BooleanResult(false));
        action.AddResult(energy.Variable, new IntegerAddResult(-EnergyConsumed));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        carpenter.GetComponentInChildren<Animator>().SetBool("Craft", true);
        hammer.SetActive(true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        carpenter.GetComponentInChildren<Animator>().SetBool("Craft", false);
        hammer.SetActive(false);
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        carpenter.Workshop.SetObject(Workshop.WorkshopObject.Crate);
    }
}

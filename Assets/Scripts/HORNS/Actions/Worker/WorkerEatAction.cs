using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class WorkerEatAction : EatAction
{
    private Worker worker;

    protected override void Start()
    {
        base.Start();
        worker = GetComponentInParent<Worker>();
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);

        action.AddPrecondition(worker.Money.Variable, new IntegerConsumePrecondition(tavern.Price));
        action.AddResult(worker.Money.Variable, new IntegerAddResult(-tavern.Price));
        action.AddResult(hunger.Variable, new IntegerAddResult(-tavern.HungerSatisfied));
    }
}

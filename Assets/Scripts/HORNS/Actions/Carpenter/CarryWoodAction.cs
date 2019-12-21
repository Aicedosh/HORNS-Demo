using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class CarryWoodAction : GoToAction
{
    public BoolVariable HasWood;
    public BoolVariable WorkshopHasWood;
    public Transform Workshop;

    protected override void Perform()
    {
        navigator.GoTo(Workshop, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(HasWood.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(WorkshopHasWood.Variable, new BooleanPrecondition(false));
        action.AddResult(WorkshopHasWood.Variable, new BooleanResult(true));
        action.AddResult(HasWood.Variable, new BooleanResult(false));
    }
}

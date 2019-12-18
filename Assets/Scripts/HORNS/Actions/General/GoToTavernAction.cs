using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class GoToTavernAction : GoToAction
{
    public Transform target;
    public IntVariable NumberOfCustomers;

    public BoolVariable IsInDestTavern;

    protected override void Perform()
    {
        navigator.GoTo(target, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(IsInDestTavern.Variable, new BooleanResult(true));
        action.AddResult(NumberOfCustomers.Variable, new IntegerAddResult(1));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        Complete();
        Debug.Log($"There are {NumberOfCustomers.Variable.Value} clients right now");
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class LeaveTavernAction : SetBoolAction
{
    public IntVariable NumberOfCustomers;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(NumberOfCustomers.Variable, new IntegerAddResult(-1));
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class PickRadishAction : BasicAction
{
    Navigator navigator;

    public Transform Target;
    public IntVariable RadishCount;

    protected override void ActionResult()
    {
        navigator.GoTo(Target, OnActionEnd);
    }

    protected override void AddResults(Action action)
    {
        base.AddResults(action);
        action.AddResult<int, IntegerAddResult, IntegerSolver, IntegerPrecondition>(new IntegerAddResult(RadishCount.LibVariable, 1), RadishCount.IntSolver);
    }

    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<Navigator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

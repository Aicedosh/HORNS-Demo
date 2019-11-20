using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class DeliverRadish : BasicAction
{
    private Navigator navigator;

    public BoolVariable HasRadish;
    public IntVariable RadishAtDestination;
    public Transform Destination;

    protected override void SetupAction(Action action)
    {
        action.AddPrecondition<bool, BooleanResult, BooleanSolver, BooleanPrecondition>(new BooleanPrecondition(HasRadish.LibVariable, true, HasRadish.BoolSolver));
        action.AddResult<bool, BooleanResult, BooleanSolver, BooleanPrecondition>(new BooleanResult(HasRadish.LibVariable, false), HasRadish.BoolSolver);
        action.AddResult<int, IntegerAddResult, IntegerSolver, IntegerPrecondition>(new IntegerAddResult(RadishAtDestination.LibVariable, 1), RadishAtDestination.IntSolver);
    }

    protected override void ActionResult()
    {
        navigator.GoTo(Destination, OnActionEnd);
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

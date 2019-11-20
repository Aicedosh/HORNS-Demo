using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class PickRadishAction : BasicAction
{
    private Navigator navigator;

    public IntVariable RadishCountOnField;
    public BoolVariable HasRadish;
    public RadishField RadishField;

    private Transform target;

    protected override void ActionResult()
    {
        target = navigator.GoToNearest(RadishField.GetAllRadishPositions(), OnActionEnd);
        RadishField.Remove(target);
    }

    protected override void OnActionEnd()
    {
        Destroy(target.gameObject);
        base.OnActionEnd();
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition<bool, BooleanResult, BooleanSolver, BooleanPrecondition>(new BooleanPrecondition(HasRadish.LibVariable, false, HasRadish.BoolSolver));
        action.AddPrecondition<int, IntegerAddResult, IntegerSolver, IntegerPrecondition>(new IntegerPrecondition(RadishCountOnField.LibVariable, 1, IntegerPrecondition.Condition.AtLeast, RadishCountOnField.IntSolver));
        action.AddResult<bool, BooleanResult, BooleanSolver, BooleanPrecondition>(new BooleanResult(HasRadish.LibVariable, true), HasRadish.BoolSolver);
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

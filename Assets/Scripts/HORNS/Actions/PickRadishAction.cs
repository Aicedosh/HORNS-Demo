﻿using HORNS;
using UnityEngine;

public class PickRadishAction : BasicAction
{
    private Navigator navigator;

    public IntVariable RadishCountOnField;
    public BoolVariable HasRadish;
    public RadishField RadishField;

    private Transform target;

    protected override void Perform()
    {
        target = navigator.GoToNearest(RadishField.GetAllRadishPositions(), OnActionEnd);
    }

    protected override void OnActionEnd(bool success)
    {
        if(success)
        {
            RadishField.Remove(target);
            Destroy(target.gameObject);
        }
        base.OnActionEnd(success);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(HasRadish.Variable, new BooleanPrecondition(false));
        action.AddPrecondition(RadishCountOnField.Variable, new IntegerPrecondition(1, IntegerPrecondition.Condition.AtLeast));
        action.AddResult(HasRadish.Variable, new BooleanResult(true));
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

using HORNS;
using UnityEngine;

public class StartRest : BasicAction
{
    public BoolVariable IsResting;

    protected override void Perform()
    {
        OnActionEnd(true);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(IsResting.Variable, new BooleanPrecondition(false));
        action.AddResult(IsResting.Variable, new BooleanResult(true));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

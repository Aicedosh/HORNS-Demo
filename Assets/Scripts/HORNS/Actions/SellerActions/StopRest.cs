using HORNS;
using UnityEngine;

public class StopRest : BasicAction
{
    private Navigator navigator;

    public BoolVariable IsResting;
    public Transform Work;

    protected override void Perform()
    {
        navigator.GoTo(Work, OnActionEnd);
    }

    protected override void OnActionEnd(bool success)
    {
        base.OnActionEnd(success);
        if (success)
        {

        }
        else
        {
            navigator.Stop();
        }
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(IsResting.Variable, new BooleanPrecondition(true));
        action.AddResult(IsResting.Variable, new BooleanResult(false));
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

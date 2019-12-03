using HORNS;
using UnityEngine;

public class GetDrink : BasicAction
{
    private Navigator navigator;

    public BoolVariable IsResting;
    public IntVariable Money;
    public IntVariable Drinks;
    public Transform Destination;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(IsResting.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(Money.Variable, new IntegerPrecondition(10, IntegerPrecondition.Condition.AtLeast));
        action.AddResult(Money.Variable, new IntegerAddResult(-10));
        action.AddResult(Drinks.Variable, new IntegerAddResult(1));
    }

    protected override void Perform()
    {
        navigator.GoTo(Destination, OnActionEnd);
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

using HORNS;
using UnityEngine;

public class DeliverRadish : BasicAction
{
    private Navigator navigator;

    public BoolVariable HasRadish;
    public BoolVariable IsLonely;
    public IntVariable RadishAtDestination;
    public Transform Destination;

    protected override void SetupAction(Action action)
    {
        action.AddPrecondition(HasRadish.Variable, new BooleanPrecondition(true));
        action.AddResult(HasRadish.Variable, new BooleanResult(false));
        action.AddResult(RadishAtDestination.Variable, new IntegerAddResult(1));
        action.AddResult(IsLonely.Variable, new BooleanResult(true));
    }

    protected override void Perform()
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

using HORNS;
using UnityEngine;

public class PickRadishAction : BasicAction
{
    private Navigator navigator;

    public IntVariable RadishCountOnField;
    public BoolVariable HasRadish;
    public RadishField RadishField;
    public IntVariable RadishPickerDurability;

    private Transform target;

    protected override void Perform()
    {
        target = navigator.GoToNearest(RadishField.GetAllRadishPositions(), OnActionEnd);
    }

    protected override void OnActionEnd(bool success)
    {
        base.OnActionEnd(success);
        if(success)
        {
            RadishField.Remove(target);
            Destroy(target.gameObject);
        }
        else
        {
            navigator.Stop();
        }
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(HasRadish.Variable, new BooleanPrecondition(false));
        action.AddPrecondition(RadishCountOnField.Variable, new IntegerPrecondition(1, IntegerPrecondition.Condition.AtLeast));
        action.AddPrecondition(RadishPickerDurability.Variable, new IntegerPrecondition(1, IntegerPrecondition.Condition.AtLeast));
        action.AddResult(HasRadish.Variable, new BooleanResult(true));
        action.AddResult(RadishCountOnField.Variable, new IntegerAddResult(-1));
        action.AddResult(RadishPickerDurability.Variable, new IntegerAddResult(-1));
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

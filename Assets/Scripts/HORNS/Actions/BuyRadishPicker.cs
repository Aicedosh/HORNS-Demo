using HORNS;
using UnityEngine;

public class BuyRadishPicker : BasicAction
{
    private Navigator navigator;

    public BoolVariable SellerWorking;
    public IntVariable RadishPickerDurability;
    public Transform Seller;

    protected override void Perform()
    {
        navigator.GoTo(Seller, OnActionEnd);
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
        action.AddPrecondition(SellerWorking.Variable, new BooleanPrecondition(true));
        action.AddResult(RadishPickerDurability.Variable, new IntegerAddResult(5));
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

using HORNS;
using UnityEngine;

public class BuyRadishPicker : BasicAction
{
    private Navigator navigator;

    public BoolVariable SellerResting;
    public IntVariable RadishPickerDurability;
    public IntVariable SellerMoney;
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
        action.AddPrecondition(SellerResting.Variable, new BooleanPrecondition(false));
        action.AddResult(RadishPickerDurability.Variable, new IntegerAddResult(5));
        action.AddResult(SellerMoney.Variable, new IntegerAddResult(3));
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

using HORNS;

public class Rest : RestIdle
{
    public override bool IsIdle => false;

    public int EnergyRestored;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(chicken.Energy.Variable, new IntegerAddResult(EnergyRestored));
    }
}

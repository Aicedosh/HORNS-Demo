using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class EatAction : BasicAction
{
    public IntVariable Hunger;
    public IntVariable Money;
    public IntVariable NumberOfCustomers;
    public BoolVariable Works;
    public BoolVariable IsInTavern;
    public float CrowdFactor;

    public int MoneyRequired;
    public int HungerSatisfied;
    public float TimeToEat;

    private float timeElapsed = 0;
    private bool eating = false;

    protected override void Perform()
    {
        eating = true;
    }

    private void Update()
    {
        if (eating)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= TimeToEat)
            {
                eating = false;
                timeElapsed = 0;
                Complete();
            }
        }
    }

    protected override void SetupAction(Action action)
    {
        if(Works != null)
        {
            action.AddPrecondition(Works.Variable, new BooleanPrecondition(false));
        }

        if (Money != null)
        {
            action.AddPrecondition(Money.Variable, new IntegerPrecondition(MoneyRequired, IntegerPrecondition.Condition.AtLeast));
            action.AddResult(Money.Variable, new IntegerAddResult(-MoneyRequired));
        }

        action.AddPrecondition(IsInTavern.Variable, new BooleanPrecondition(true));

        action.AddResult(Hunger.Variable, new IntegerAddResult(-HungerSatisfied));

        action.AddCost(NumberOfCustomers.Variable, n => (n - (IsInTavern.Variable.Value ? 1 : 0)) * CrowdFactor);
    }
}

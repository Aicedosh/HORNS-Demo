using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public abstract class EatAction : BasicAction
{
    protected IntVariable hunger;
    private IntVariable numberOfCustomers;
    private BoolVariable isInTavern;

    protected Tavern tavern;

    public float CrowdFactor;

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
        action.AddPrecondition(isInTavern.Variable, new BooleanPrecondition(true));

        action.AddCost(numberOfCustomers.Variable, n => (n - (isInTavern.Variable.Value ? 1 : 0)) * CrowdFactor);
    }

    protected virtual void Start()
    {
        hunger = GetComponentInParent<BasicAgent>().Hunger;
        numberOfCustomers = GetComponentInParent<TavernClient>().Tavern.NumberOfCustomers;
        isInTavern = GetComponentInParent<TavernClient>().IsInTavern;
        tavern = GetComponentInParent<TavernClient>().Tavern;
    }
}

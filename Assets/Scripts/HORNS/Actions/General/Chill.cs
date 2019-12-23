using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class Chill : GoToAction
{
    private IntVariable energy;
    private Transform home;

    public int StartValue;
    public int EnergyRestored;

    protected override void Perform()
    {
        navigator.GoTo(home, OnWalkEnd);
    }

    protected override void Start()
    {
        base.Start();
        energy.Variable.Value = StartValue;

        energy = GetComponentInParent<Worker>().Energy;
        home = GetComponentInParent<Worker>().Home.Spot;
    }

    public override bool IsIdle => false;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddCost(EnergyRestored);
        action.AddResult(energy.Variable, new IntegerAddResult(15));
    }
}

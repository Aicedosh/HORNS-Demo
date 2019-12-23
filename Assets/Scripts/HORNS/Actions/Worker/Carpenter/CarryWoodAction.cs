﻿using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class CarryWoodAction : GoToAction
{
    private BoolVariable hasWood;
    private BoolVariable workshopHasWood;
    private Transform workspot;

    private BasicAgent basicAgent;

    protected override void Start()
    {
        base.Start();
        Carpenter carpenter = GetComponentInParent<Carpenter>();
        hasWood = carpenter.HasWood;
        workshopHasWood = carpenter.Workshop.HasWood;
        workspot = carpenter.Workshop.Spot;

        basicAgent = GetComponentInParent<BasicAgent>();
    }

    protected override void Perform()
    {
        navigator.GoTo(workspot, OnWalkEnd);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(hasWood.Variable, new BooleanPrecondition(true));
        action.AddPrecondition(workshopHasWood.Variable, new BooleanPrecondition(false));
        action.AddResult(workshopHasWood.Variable, new BooleanResult(true));
        action.AddResult(hasWood.Variable, new BooleanResult(false));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Carry", false);
    }
}

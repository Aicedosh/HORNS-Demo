using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class BuyWoodAction : GoToAction
{
    private IntVariable shopWoodCount;
    private Transform shopspot;
    private BoolVariable isShopOpen;
    private BoolVariable hasWood;
    private BoolVariable hasCrate;
    private GameObject logGo;

    private BasicAgent basicAgent;

    protected override void Start()
    {
        base.Start();
        ObjectSeller objectSeller = GetComponentInParent<ObjectSeller>();

        shopWoodCount = objectSeller.Shop.WoodCount;
        shopspot = objectSeller.Shop.ClientSpot;
        isShopOpen = objectSeller.Shop.IsOpen;
        hasWood = GetComponentInParent<Carpenter>().HasWood;
        hasCrate = GetComponentInParent<Carpenter>().HasCrate;
        logGo = GetComponentInParent<Carpenter>().Log;

        basicAgent = GetComponentInParent<BasicAgent>();
    }

    protected override void Perform()
    {
        navigator.GoTo(shopspot, OnWalkEnd);
        basicAgent.GetComponentInChildren<Carrier>().SetCarriedObject(logGo);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(hasWood.Variable, new BooleanPrecondition(false));
        action.AddPrecondition(hasCrate.Variable, new BooleanPrecondition(false));
        action.AddPrecondition(shopWoodCount.Variable, new IntegerConsumePrecondition(1));
        action.AddPrecondition(isShopOpen.Variable, new BooleanPrecondition(true));
        action.AddResult(hasWood.Variable, new BooleanResult(true));
        action.AddResult(shopWoodCount.Variable, new IntegerAddResult(-1));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        basicAgent.GetComponentInChildren<Animator>().SetBool("Carry", true);
    }
}

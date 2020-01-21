using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWoodReactor : VariableReactor<int>
{
    protected override void Start()
    {
        base.Start();
        GetComponentInParent<ObjectSeller>().Shop.WoodCount.Variable.Observe(this);
    }

    protected override bool ShouldRecalculate(int oldValue, int newValue)
    {
        //TODO: Recalc only if changed to or from 0!
        Carpenter carpenter = GetComponentInParent<Carpenter>();
        return carpenter.HasWood.Variable.Value == false &&
            carpenter.HasCrate.Variable.Value == false &&
            carpenter.Workshop.HasWood.Variable.Value == false &&
            carpenter.Workshop.HasCrate.Variable.Value == false &&
            ((oldValue == 0 && newValue == 1) || (oldValue == 1 && newValue == 0));
    }
}

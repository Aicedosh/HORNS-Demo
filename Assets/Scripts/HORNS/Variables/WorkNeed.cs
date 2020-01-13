using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkNeed : BoolNeed
{
    private BoolVariable isShopOpen;

    public override DemoVariable<bool> GenericVariable => isShopOpen;

    public override LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().BoolNeedPrefab;
        var go = Instantiate(canvas);
        go.GetComponent<BooleanNeedDisplay>().Need = this;
        return go;
    }

    protected override float Evaluate(bool value)
    {
        return value ? TrueValue : FalseValue;
    }

    private void Start()
    {
        isShopOpen = GetComponentInParent<Merchant>().Shop.IsOpen;
    }
}

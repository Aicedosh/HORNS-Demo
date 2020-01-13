using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveWorkAction : SetBoolAction
{
    protected override void Start()
    {
        base.Start();
        Variable = GetComponentInParent<Merchant>().Shop.IsOpen;
    }
}

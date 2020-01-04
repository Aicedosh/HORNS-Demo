using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PutObjectDownAction : GoToAction
{
    protected abstract BoolVariable CarriesObject { get; }


}

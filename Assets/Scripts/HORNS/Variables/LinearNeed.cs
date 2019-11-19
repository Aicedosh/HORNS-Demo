using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearNeed : Need<int>
{
    public float ValueFactor;

    public IntVariable Variable;
    public override Variable<int> GenericVariable => Variable;

    protected override float Evaluate(int value)
    {
        return ValueFactor * value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

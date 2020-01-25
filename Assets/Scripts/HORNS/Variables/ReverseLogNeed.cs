using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ReverseLogNeed : LogNeed
{
    public float ZeroLevel;

    protected override float Evaluate(int value)
    {
        if(value > ZeroLevel)
        {
            return -Multiplier * value;
        }
        return Multiplier * Mathf.Log10(-value + 1 + ZeroLevel);
    }
}

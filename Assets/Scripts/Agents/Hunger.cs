using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public IntVariable HungerVariable;
    public float TimeToChange;
    public int Amount;

    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= TimeToChange)
        {
            timeElapsed -= TimeToChange;
            HungerVariable.Variable.Value += Amount;
            Debug.Log(HungerVariable.Variable.Value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public float TimeToChange;
    public int Amount;

    private float timeElapsed;
    private IntVariable hunger;

    private void Start()
    {
        hunger = GetComponent<BasicAgent>().Hunger;
        hunger.Variable.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= TimeToChange)
        {
            timeElapsed -= TimeToChange;
            hunger.Variable.Value += Amount;
        }
    }
}

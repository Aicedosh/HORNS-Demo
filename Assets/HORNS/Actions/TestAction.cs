using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : BasicAction
{
    private bool working = false;
    private float time = 0f;

    private int count = 0;

    public float Interval;

    //TODO: this name is not the best (change in HORNS)
    protected override void ActionResult()
    {
        working = true;
        Debug.Log($"Hello Action {count}");
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= Interval)
        {
            time -= Interval;
            Debug.Log($"Goodbye Action {count++}");
            working = false;
            OnActionEnd();
        }
    }
}

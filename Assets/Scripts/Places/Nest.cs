using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public Transform GetRandomLocationNearNest(float minDIst, float maxDist)
    {
        float r = Random.Range(minDIst, maxDist);
        Transform t = gameObject.transform.GetChild(0);
        Vector2 pos = Random.insideUnitCircle.normalized * r;
        t.localPosition = new Vector3(pos.x, 0, pos.y);
        return t;
    }
}

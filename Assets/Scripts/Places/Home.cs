using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Home : MonoBehaviour
{
    public Transform Spot;

    public int NumberOfResidents { get; set; }

    public T[] GetClosest<T>(int n, Func<T, bool> predicate = null) where T : MonoBehaviour
    {
        if(predicate == null)
        {
            predicate = t => true;
        }
        return FindObjectsOfType<T>().Where(predicate).Select(h => (h, Vector3.Distance(h.transform.position, transform.position)))
            .OrderBy(t => t.Item2).Take(n).Select(t => t.h).ToArray();
    }

    public T GetClosest<T>(Func<T, bool> predicate = null) where T : MonoBehaviour
    {
        return GetClosest(1, predicate)[0];
    }
}

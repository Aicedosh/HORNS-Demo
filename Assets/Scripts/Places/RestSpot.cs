using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestSpot : MonoBehaviour
{
    public IntVariable CrowdSize;
    public IEnumerable<Transform> Spots => Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i).transform);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RadishField : SpawnOnClick
{
    private List<GameObject> radishes = new List<GameObject>();

    public override void OnClick(Vector3 position)
    {
        radishes.Add(SpawnWithRandomRotation(position));
    }

    public IEnumerable<Transform> GetAllRadishPositions()
    {
        return radishes.Select(r => r.transform);
    }

    public void Remove(Transform transform)
    {
        Destroy(transform.gameObject);
        radishes.Remove(transform.gameObject);
    }
}

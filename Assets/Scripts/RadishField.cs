using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RadishField : SpawnOnClick
{
    private List<GameObject> radishes = new List<GameObject>();

    public IntVariable RadishCount;

    public override void OnClick(Vector3 position)
    {
        radishes.Add(SpawnWithRandomRotation(position));
        RadishCount.LibVariable.Value++;
    }

    public IEnumerable<Transform> GetAllRadishPositions()
    {
        return radishes.Select(r => r.transform);
    }

    public void Remove(Transform transform)
    {
        radishes.Remove(transform.gameObject);
        RadishCount.LibVariable.Value--;
    }
}

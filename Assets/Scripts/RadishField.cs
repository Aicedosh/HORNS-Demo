using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RadishField : MonoBehaviour
{
    public int MinRadishCount;
    public int MaxRadishCount;

    public float MinRange;
    public float MaxRange;

    public GameObject RadishPrefab;

    public IEnumerable<Transform> Spots => Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i));

    private void Start()
    {
        foreach (var s in Spots)
        {
            float r = Random.Range(MinRange, MaxRange);
            int c = Random.Range(MinRadishCount, MaxRadishCount);

            for (int i = 0; i < c; i++)
            {
                Vector2 offset = Random.insideUnitCircle * r;
                var go = Instantiate(RadishPrefab, Vector3.zero,
                    Quaternion.Euler(RadishPrefab.transform.rotation.eulerAngles.x, Random.Range(0f, 360f), RadishPrefab.transform.rotation.eulerAngles.z));
                go.transform.parent = s;
                go.transform.localPosition = new Vector3(offset.x, 0, offset.y);
            }
        }
    }
}

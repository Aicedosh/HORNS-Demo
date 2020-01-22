using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

public class Forest : MonoBehaviour
{
    public float Length;
    public float Width;
    public int NumberOfTreesLenght;
    public int NumberOfTreesWidth;

    public float Padding;
    public float MinRespawnTime;
    public float MaxRespawnTime;

    private float timeSinceRegrowth = 0f;
    private int totalTreesCount;

    public GameObject[] Prefabs;

    private List<(float, float, GameObject)> trees = new List<(float, float, GameObject)>();
    private List<(float, float)> locations = new List<(float, float)>();

    private float _dx;
    private float _dz;

    private void SpawnAt(float x, float z)
    {
        float dx = _dx / 2 - Padding;
        float dz = _dz / 2 - Padding;

        GameObject prefab = Prefabs[Random.Range(0, Prefabs.Length - 1)];
        float nx = Random.Range(x - dx, x + dx);
        float nz = Random.Range(z - dz, z + dz);

        Quaternion originalRotation = prefab.transform.rotation;
        Quaternion randomRotation = Quaternion.Euler(originalRotation.eulerAngles.x, Random.Range(0, 360), originalRotation.eulerAngles.z);

        GameObject go = Instantiate(prefab, transform);
        go.transform.localPosition = new Vector3(nx, transform.position.y, nz);
        go.transform.localRotation = randomRotation;
        trees.Add((x, z, go));
    }

    void Start()
    {
        _dx = (Length / NumberOfTreesLenght);
        _dz = (Width / NumberOfTreesWidth);
        for (float x = -Length/2 + Padding; x < Length/2; x += _dx)
        {
            for (float z = -Width/2 + Padding; z < Width/2; z += _dz)
            {
                if (IsInElipse(x, z, Length/2, Width/2))
                {
                    totalTreesCount++;
                    SpawnAt(x, z);
                }
            }
        }
    }

    private bool IsInElipse(float x, float y, float rx, float ry)
    {
        return (x * x) / (rx * rx) + (y * y) / (ry * ry) <= 1;
    }

    private void Update()
    {
        if(locations.Count > 0)
        {
            timeSinceRegrowth += Time.deltaTime;
        }

        if (timeSinceRegrowth >= Mathf.Lerp(MaxRespawnTime, MinRespawnTime, ((float)locations.Count) / totalTreesCount))
        {
            timeSinceRegrowth = 0f; 
            int idx = Random.Range(0, locations.Count);
            var loc = locations[idx];
            SpawnAt(loc.Item1, loc.Item2);
            locations.RemoveAt(idx);
        }
    }

    public List<Transform> GetTreesLocations()
    {
        var res = new List<Transform>();
        foreach (var tree in trees)
        {
            if(tree.Item3.gameObject.GetComponent<Tree>().Chopper == null)
            {
                res.Add(tree.Item3.transform);
            }
        }

        return res;
    }

    public void Remove(Transform tree)
    {
        List<(float, float, GameObject)> newList = new List<(float, float, GameObject)>();
        foreach(var t in trees)
        {
            if(t.Item3 != null && tree != null)
            {
                if (t.Item3.transform == tree.transform)
                {
                    locations.Add((t.Item1, t.Item2));
                }
                else
                {
                    newList.Add(t);
                }
            }
        }
        trees = newList;
    }
}

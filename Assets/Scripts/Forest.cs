using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    public float Length;
    public float Width;
    public int NumberOfTreesLenght;
    public int NumberOfTreesWidth;

    public float Padding;

    public GameObject[] Prefabs;

    private List<GameObject> trees = new List<GameObject>();

    private void SpawnAt(float x, float z, float dx, float dz)
    {
        GameObject prefab = Prefabs[Random.Range(0, Prefabs.Length - 1)];
        float nx = Random.Range(x - dx, x + dx);
        float nz = Random.Range(z - dz, z + dz);

        Quaternion originalRotation = prefab.transform.rotation;
        Quaternion randomRotation = Quaternion.Euler(originalRotation.eulerAngles.x, Random.Range(0, 360), originalRotation.eulerAngles.z);

        GameObject go = Instantiate(prefab, transform);
        go.transform.localPosition = new Vector3(nx, transform.position.y, nz);
        go.transform.localRotation = randomRotation;
        trees.Add(go);
    }

    // Start is called before the first frame update
    void Start()
    {
        float dx = (Length / NumberOfTreesLenght);
        float dz = (Width / NumberOfTreesWidth);
        for (float x = -Length/2 + Padding; x < Length/2; x += dx)
        {
            for (float z = -Width/2 + Padding; z < Width/2; z += dz)
            {
                SpawnAt(x, z, dx / 2 - Padding, dz / 2 - Padding);
            }
        }
    }
}

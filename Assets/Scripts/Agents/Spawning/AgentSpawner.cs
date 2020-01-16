using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject[] AgentPrefabs;
    public int Count;

    private SimplePriorityQueue<Home> homes;

    void Start()
    {
        homes = new SimplePriorityQueue<Home>();
        foreach(Home h in FindObjectsOfType<Home>())
        {
            homes.Enqueue(h, h.NumberOfResidents);
        }

        Spawn(Count);
    }

    private GameObject GetPrefab()
    {
        return AgentPrefabs[Random.Range(0, AgentPrefabs.Length)];
    }

    private void SpawnAgent(GameObject prefab, Home home)
    {
        GameObject go = Instantiate(prefab, home.Spot.transform.position, Quaternion.identity);
        go.transform.parent = transform;

        foreach(IAgentConfigure conf in go.GetComponentsInChildren<IAgentConfigure>())
        {
            conf.Configure(home);
        }
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Home h = homes.First;

            SpawnAgent(GetPrefab(), h);
            ++h.NumberOfResidents;
            homes.UpdatePriority(h, h.NumberOfResidents);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicAgent : MonoBehaviour
{
    public float DangerDistance = 15f;

    public IntVariable Hunger;
    public BoolVariable IsNearDanger;
    public IEnumerable<BoolVariable> IsInTavernVariables => GetComponentsInChildren<TavernClient>().Select(tc => tc.IsInTavern);

    public bool RunsAway;
    public GameObject RunSpot;

    private GameObject enemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if(IsNearDanger != null)
        {
            IsNearDanger.Variable.Value = (enemy.transform.position - this.transform.position).magnitude <= DangerDistance;
        }

        if(RunSpot != null)
        {
            Vector3 dir = enemy.transform.position - this.transform.position;
            dir.y = 0;
            dir = -100 * dir.normalized;
            Vector3 offset = new Vector3(dir.x, 3f, dir.z);
            RunSpot.transform.SetPositionAndRotation(transform.position + offset, Quaternion.identity);
        }
    }
}

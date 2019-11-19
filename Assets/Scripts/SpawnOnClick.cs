using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : Clickable
{
    public GameObject Prefab;
    public override void OnClick(Vector3 position)
    {
        Quaternion originalRotation = Prefab.transform.rotation;
        Quaternion randomRotation = Quaternion.Euler(originalRotation.eulerAngles.x, Random.Range(0, 360), originalRotation.eulerAngles.z);
        Instantiate(Prefab, position, randomRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class GoToAction : BasicAction
{
    protected override void Perform()
    {
        navigator.GoTo(Destination, Arrived);
    }

    private void Arrived(bool success)
    {
        if(!success)
        {
            OnActionEnd(false);
            return;
        }

        arrived = true;
    }

    public Transform Destination;

    public float TimeToComplete;

    private Navigator navigator;
    private SkinnedMeshRenderer _renderer;
    private CapsuleCollider _collider;
    private float timeElapsed;
    private bool arrived;
    private bool eating;

    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<Navigator>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(arrived)
        {
            _renderer.enabled = false;
            _collider.enabled = false;
            arrived = false;
            timeElapsed = 0;
            eating = true;
        }

        if(eating)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= TimeToComplete)
            {
                eating = false;
                _renderer.enabled = true;
                _collider.enabled = true;
                OnActionEnd(true);
            }
        }
    }
}

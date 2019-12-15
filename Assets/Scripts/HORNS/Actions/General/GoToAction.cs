using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoToAction : BasicAction
{
    public float TimeToComplete;
    public bool Hide;

    protected Navigator navigator;
    private SkinnedMeshRenderer _renderer;
    private CapsuleCollider _collider;
    private NavMeshAgent _nav;
    private float timeElapsed;
    private bool arrived;

    private float prevRadius;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        navigator = GetComponent<Navigator>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _collider = GetComponent<CapsuleCollider>();
        _nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(arrived)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= TimeToComplete)
            {
                arrived = false;

                if (Hide)
                {
                    transform.Translate(new Vector3(0, 100, 0), Space.World);
                }

                Complete();
            }
        }
    }

    protected void OnWalkEnd(bool success)
    {
        if(success)
        {
            timeElapsed = 0;
            arrived = true;

            if (Hide)
            {
                transform.Translate(new Vector3(0, -100, 0), Space.World);
            }

            OnArrive();
        }
        else
        {
            Cancel();
        }
    }

    protected override void OnCancel()
    {
        base.OnCancel();
        navigator.Stop();
    }

    //Start animation etc.
    protected virtual void OnArrive() { }
}

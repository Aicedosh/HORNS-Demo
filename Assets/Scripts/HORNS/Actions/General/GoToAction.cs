using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public abstract class GoToAction : BasicAction
{
    public float TimeToComplete;
    public bool Hide;

    protected Navigator navigator;
    private SkinnedMeshRenderer _renderer;
    private CapsuleCollider _collider;
    private float timeElapsed;
    private bool arrived;

    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<Navigator>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _collider = GetComponent<CapsuleCollider>();
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
                    _renderer.enabled = true;
                    _collider.enabled = true;
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
                _renderer.enabled = false;
                _collider.enabled = false;
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

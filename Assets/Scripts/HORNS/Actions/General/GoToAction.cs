﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HORNS;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoToAction : BasicAction
{
    public float TimeToComplete;
    public bool Hide;

    protected Navigator navigator;
    private CapsuleCollider _collider;
    private NavMeshAgent _nav;
    private float timeElapsed;
    private bool arrived;

    private float prevRadius;

    private BoolVariable[] isInTavernVariables;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);

        foreach(var v in isInTavernVariables)
        {
            action.AddPrecondition(v.Variable, new BooleanPrecondition(false));
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        navigator = GetComponentInParent<Navigator>();
        _collider = GetComponentInParent<CapsuleCollider>();
        _nav = GetComponentInParent<NavMeshAgent>();

        isInTavernVariables = GetComponentInParent<BasicAgent>().IsInTavernVariables.ToArray();
    }

    private void SetAgentVisibility(bool visible)
    {
        //_renderer.enabled = visible;
        _collider.enabled = visible;
        if(!visible)
        {
            prevRadius = _nav.radius;
            _nav.radius = 0;
        }
        else
        {
            _nav.radius = prevRadius;
        }
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
                    SetAgentVisibility(true);
                }

                FinishWork();
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
                SetAgentVisibility(false);
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
        arrived = false;
    }

    //Start animation etc.
    protected virtual void OnArrive() { }

    protected virtual void FinishWork() { Complete(); }
}

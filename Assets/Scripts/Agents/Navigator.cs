using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;

    private System.Action<bool> finishCallback;
    private GameObject currentTarget;
    private bool isWalking = false;
    private bool isFollowing = false;

    public float GoalDistance;
    public float WalkAnimationTreshold;

    public float Speed = 1.3f;
    public float WalkSpeedChangeIntervalMinSec = 2.5f;
    public float WalkSpeedChangeIntervalMaxSec = 7f;
    public float WalkSpeedChangeTimeMinSec = 1.5f;
    public float WalkSpeedChangeTimeMaxSec = 2f;
    public float WalkSpeedDeltaPerc = 0.05f;

    private float timeToChange = 0f;
    private float changingTime;
    private float timeSinceChange;
    private float currentSpeed;
    private float speedDelta;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = GoalDistance;
    }

    private void ChangeSpeed()
    {
        if(timeToChange <= 0f)
        {
            timeToChange = Random.Range(WalkSpeedChangeIntervalMinSec, WalkSpeedChangeIntervalMaxSec);
            changingTime = Random.Range(WalkSpeedChangeTimeMinSec, WalkSpeedChangeTimeMaxSec);
            speedDelta = Random.Range(1 - WalkSpeedDeltaPerc, 1 + WalkSpeedDeltaPerc);
            timeSinceChange = 0f;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, speedDelta * Speed, timeSinceChange/changingTime);

        nav.speed = currentSpeed;
        anim.speed = currentSpeed / Speed;
        timeToChange -= Time.deltaTime;
        timeSinceChange += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();

        if(isFollowing)
        {
            nav.destination = currentTarget.transform.position;
        }

        if(isWalking)
        {
            if(currentTarget == null)
            {
                Stop(false);
            }
            else if(nav.pathPending == false && nav.remainingDistance <= GoalDistance)
            {
                Stop(true);
            }
        }

        anim.SetBool("Walk", nav.velocity.magnitude > WalkAnimationTreshold);
    }

    private void Stop(bool success)
    {
        Stop();
        finishCallback?.Invoke(success);
    }

    public void Stop()
    {
        isWalking = false;
        isFollowing = false;
        nav.isStopped = false;
    }

    public bool GoTo(Transform transform, System.Action<bool> finishCallback = null)
    {
        if (isWalking)
        {
            return false;
        }

        this.finishCallback = finishCallback;
        this.currentTarget = transform.gameObject;
        nav.SetDestination(transform.position);
        isWalking = true;
        
        return true;
    }

    public bool Follow(Transform transform, System.Action<bool> finishCallback = null)
    {
        bool ret = GoTo(transform, finishCallback);
        if (ret)
        {
            isFollowing = true;
        }
        return ret;
    }

    public Transform GoToNearest(IEnumerable<Transform> transforms, System.Action<bool> finishCallback = null)
    {
        if (isWalking)
        {
            return null;
        }

        float dist = float.PositiveInfinity;
        Transform nearest = null;
        foreach(Transform transform in transforms)
        {
            float d = GetDistanceTo(transform.position);
            if(d < dist)
            {
                dist = d;
                nearest = transform;
            }
        }

        return nearest != null && GoTo(nearest, finishCallback) ? nearest : null;
    }

    private float GetDistanceTo(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        nav.CalculatePath(transform.position, path);
        float dist = 0f;

        if ((path.status != NavMeshPathStatus.PathInvalid))
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                dist += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }
        dist += Vector3.Distance(path.corners[path.corners.Length - 1], pos);

        return dist;
    }
}

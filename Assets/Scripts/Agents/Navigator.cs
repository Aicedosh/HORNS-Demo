using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;


    private System.Action finishCallback;
    private bool isWalking = false;

    public float GoalDistance;
    public float WalkAnimationTreshold;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = GoalDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking && nav.pathPending == false && nav.remainingDistance <= GoalDistance)
        {
            isWalking = false;
            finishCallback?.Invoke();
            nav.isStopped = false;
        }

        anim.SetBool("Walk", nav.velocity.magnitude > WalkAnimationTreshold);
    }

    public bool GoTo(Transform transform, System.Action finishCallback = null)
    {
        if (isWalking)
        {
            return false;
        }

        this.finishCallback = finishCallback;
        nav.SetDestination(transform.position);
        isWalking = true;
        
        return true;
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

using HORNS;
using UnityEngine;

public class RestIdle : GoToAction
{
    public override bool IsIdle => true;
    
    public float MinDistanceFromNest;
    public float MaxDistanceFromNest;

    private Nest nest;
    private Animator animator;

    protected Chicken chicken;

    protected override void Start()
    {
        base.Start();
        nest = FindObjectOfType<Nest>();
        chicken = GetComponentInParent<Chicken>();
        animator = chicken.GetComponentInChildren<Animator>();
    }

    protected override void Perform()
    {
        base.Perform();
        navigator.GoTo(nest.GetRandomLocationNearNest(MinDistanceFromNest, MaxDistanceFromNest), OnWalkEnd);
        animator.SetBool("Run", true);
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(chicken.IsAtNest.Variable, new BooleanPrecondition(true));
    }

    protected override void OnArrive()
    {
        base.OnArrive();
        animator.SetBool("Run", false);
        animator.SetBool("Eat", true);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        animator.SetBool("Eat", false);
    }
}

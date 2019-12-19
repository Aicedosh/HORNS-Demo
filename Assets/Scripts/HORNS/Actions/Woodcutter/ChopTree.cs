using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class ChopTree : GoToAction
{
    public Forest Forest;

    public IntVariable Energy;
    public BoolVariable Wood;

    public int EnergyLost;

    public float TimeToPickup;

    private Transform target;
    private bool isPickingUp;
    private float chopTimeElapsed;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(Wood.Variable, new BooleanPrecondition(false));
        action.AddResult(Wood.Variable, new BooleanResult(true));
        action.AddResult(Energy.Variable, new IntegerAddResult(-EnergyLost));
    }

    protected override void Perform()
    {
        target = navigator.GoToNearest(Forest.GetTreesLocations(), OnWalkEnd);
        GetComponentInChildren<Carrier>().SetCarriedObject(GetComponent<Woodcutter>().Log);
    }

    protected override void OnComplete()
    {
        Forest.Remove(target);
        Destroy(target.gameObject);
    }

    protected override void OnActionEnd()
    {
        GetComponentInChildren<Animator>().SetBool("Chop", false);
    }

    protected override void Update()
    {
        base.Update();
        if(agentAI.CurrentAction == this)
        {
            if (target == null || (target.gameObject.GetComponent<Tree>().Chopper != null && target.gameObject.GetComponent<Tree>().Chopper != agentAI))
            {
                //Someone else is either chopping our tree, or already chopped. Abort
                Cancel();
            }
        }

        if(isPickingUp)
        {
            chopTimeElapsed += Time.deltaTime;
            if(chopTimeElapsed >= TimeToPickup)
            {
                Complete();
                isPickingUp = false;
            }
        }
    }

    protected override void OnArrive()
    {
        Tree tree = target.gameObject.GetComponent<Tree>();
        if (tree.Chopper != null && tree.Chopper != agentAI)
        {
            Cancel();
            return;
        }
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Chop", true);
        tree.Chopper = agentAI;
    }

    protected override void FinishWork()
    {
        GetComponentInChildren<Animator>().SetBool("Carry", true);
        isPickingUp = true;
        chopTimeElapsed = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class ChopTree : GoToAction
{
    private Forest forest;

    private IntVariable energy;
    private BoolVariable wood;

    public int EnergyLost;

    public float TimeToPickup;

    private Transform target;
    private bool isPickingUp;
    private float chopTimeElapsed;

    private Woodcutter woodcutter;

    protected override void Start()
    {
        base.Start();
        forest = GetComponentInParent<Woodcutter>().Forest;
        energy = GetComponentInParent<Worker>().Energy;
        woodcutter = GetComponentInParent<Woodcutter>();
        wood = woodcutter.HasWood;
    }

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(wood.Variable, new BooleanPrecondition(false));
        action.AddResult(wood.Variable, new BooleanResult(true));
        action.AddResult(energy.Variable, new IntegerAddResult(-EnergyLost));
    }

    protected override void Perform()
    {
        target = navigator.GoToNearest(forest.GetTreesLocations(), OnWalkEnd);

        woodcutter.GetComponentInChildren<Carrier>().SetCarriedObject(woodcutter.Log);
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        forest.Remove(target);
        Destroy(target.gameObject);
    }

    protected override void OnActionEnd()
    {
        base.OnActionEnd();
        woodcutter.GetComponentInChildren<Animator>().SetBool("Chop", false);
        isPickingUp = false;
    }

    protected override void OnCancel()
    {
        base.OnCancel();
        target.gameObject.GetComponent<Tree>().Chopper = null;
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
        woodcutter.GetComponentInChildren<Animator>().SetBool("Chop", true);
        tree.Chopper = agentAI;
    }

    protected override void FinishWork()
    {
        woodcutter.GetComponentInChildren<Animator>().SetBool("Carry", true);
        isPickingUp = true;
        chopTimeElapsed = 0;
    }
}

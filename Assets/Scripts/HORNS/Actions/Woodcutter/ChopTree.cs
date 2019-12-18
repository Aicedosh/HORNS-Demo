using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class ChopTree : GoToAction
{
    public Forest Forest;

    public IntVariable Energy;
    public IntVariable Wood;

    public int EnergyLost;
    public int WoodGained;

    private Transform target;

    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddResult(Wood.Variable, new IntegerAddResult(WoodGained));
        action.AddResult(Energy.Variable, new IntegerAddResult(-EnergyLost));
    }

    protected override void Perform()
    {
        target = navigator.GoToNearest(Forest.GetTreesLocations(), OnWalkEnd);
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
    }

    protected override void OnArrive()
    {
        Tree tree = target.gameObject.GetComponent<Tree>();
        if (tree.Chopper != null && tree.Chopper != agentAI)
        {
            return;
        }
        base.OnArrive();
        GetComponentInChildren<Animator>().SetBool("Chop", true);
        tree.Chopper = agentAI;
    }
}

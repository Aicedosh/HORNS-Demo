using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class BotherFriend : BasicAction
{
    private List<LocateMe> friends;
    private Navigator navigator;

    public BoolVariable IsLonely;

    protected override void Perform()
    {
        navigator.Follow(friends[Random.Range(0, friends.Count)].GetTransform(), OnActionEnd);
    }

    protected override void OnActionEnd(bool success)
    {
        base.OnActionEnd(success);
        if (success)
        {

        }
        else
        {
            navigator.Stop();
        }
    }


    protected override void SetupAction(Action action)
    {
        base.SetupAction(action);
        action.AddPrecondition(IsLonely.Variable, new BooleanPrecondition(true));
        action.AddResult(IsLonely.Variable, new BooleanResult(false));
    }

    // Start is called before the first frame update
    void Start()
    {
        friends = new List<LocateMe>(FindObjectsOfType<LocateMe>());
        friends.Remove(GetComponent<LocateMe>());
        navigator = GetComponent<Navigator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

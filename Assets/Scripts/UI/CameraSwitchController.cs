using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraSwitchController : MonoBehaviour
{
    CinemachineBrain brain;
    public PlayableDirector DollyDirector;

    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
        DollyDirector.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            brain.enabled = true;
            DollyDirector.Play();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            brain.enabled = false;
            DollyDirector.Stop();
        }
    }
}

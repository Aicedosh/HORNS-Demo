using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraSwitchController : MonoBehaviour
{
    PlayableDirector townDirector;
    public CinemachineVirtualCamera TownCam;

    public CinemachineVirtualCamera FollowCam;

    // Start is called before the first frame update
    void Start()
    {
        townDirector = TownCam.GetComponent<PlayableDirector>();
        townDirector.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TownCam.Priority = 20;
            townDirector.Play();

            FollowCam.Priority = 10;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            TownCam.Priority = 10;
            townDirector.Stop();

            FollowCam.Priority = 20;
        }
    }
}

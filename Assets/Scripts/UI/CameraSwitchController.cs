using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

// not used for now
public class CameraSwitchController : MonoBehaviour
{
    PlayableDirector townDirector;
    public CinemachineVirtualCamera TownCam;

    public CinemachineVirtualCamera FollowCam;

    PlayableDirector rainDirector;
    public CinemachineVirtualCamera RainCam;

    // Start is called before the first frame update
    void Start()
    {
        townDirector = TownCam.GetComponent<PlayableDirector>();
        townDirector.Stop();
        rainDirector = RainCam.GetComponent<PlayableDirector>();
        rainDirector.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TownCam.Priority = 20;
            townDirector.Stop();
            townDirector.Play();

            FollowCam.Priority = 10;

            RainCam.Priority = 10;
            rainDirector.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            TownCam.Priority = 10;
            townDirector.Stop();

            FollowCam.Priority = 20;

            RainCam.Priority = 10;
            rainDirector.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            TownCam.Priority = 10;
            townDirector.Stop();

            FollowCam.Priority = 10;

            RainCam.Priority = 20;
            rainDirector.Stop();
            rainDirector.Play();
        }
    }
}

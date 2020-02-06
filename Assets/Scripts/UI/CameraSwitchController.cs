using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraSwitchController : MonoBehaviour
{
    PlayableDirector townDirector;
    public CinemachineVirtualCamera TownCam;
    public double TownTime;

    PlayableDirector slideDirector;
    public CinemachineVirtualCamera SlideCam;
    public double SlideTime;

    // Start is called before the first frame update
    void Start()
    {
        townDirector = TownCam.GetComponent<PlayableDirector>();
        townDirector.Stop();

        slideDirector = SlideCam.GetComponent<PlayableDirector>();
        slideDirector.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            TownCam.enabled = false;
            townDirector.Stop();

            SlideCam.enabled = false;
            slideDirector.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TownCam.enabled = true;
            StartPlay(townDirector, TownTime);

            SlideCam.enabled = false;
            slideDirector.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            TownCam.enabled = false;
            townDirector.Stop();

            SlideCam.enabled = true;
            StartPlay(slideDirector, SlideTime);
        }
    }

    void StartPlay(PlayableDirector director, double time)
    {
        director.Stop();
        director.RebuildGraph();
        director.playableGraph.GetRootPlayable(0).SetSpeed(director.duration / time);
        director.Play();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsGo;

    private void Start()
    {
        if(CommandLineParser.SkipMenu)
        {
            Play();
        }
    }

    public void Play()
    {
        Loading.LoadScene("DemoScene");
    }

    public void Options()
    {
        gameObject.SetActive(false);
        OptionsGo.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

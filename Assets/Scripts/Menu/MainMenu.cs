using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Loading.LoadScene("DemoScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

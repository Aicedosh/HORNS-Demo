using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsGo;

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

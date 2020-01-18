using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private static string SceneName = "";
    public static void LoadScene(string name)
    {
        SceneName = name;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(SceneName);
    }
}

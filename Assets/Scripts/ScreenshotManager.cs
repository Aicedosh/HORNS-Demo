using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            System.IO.Directory.CreateDirectory("Screenshots");
            string path = $"Screenshots/Screenshot-{System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff")}.png";
            Debug.Log($"Saving screenshot to {path}");
            ScreenCapture.CaptureScreenshot(path);
        }
    }
}

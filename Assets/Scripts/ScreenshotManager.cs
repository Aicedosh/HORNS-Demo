using System.IO;
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
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            Directory.CreateDirectory(dirPath);
            string filePath = Path.Combine(dirPath, $"Screenshot-{System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff")}.png");
            Debug.Log($"Saving screenshot to {filePath}");
            ScreenCapture.CaptureScreenshot(filePath);
        }
    }
}

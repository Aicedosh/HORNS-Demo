using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Build : MonoBehaviour
{
    static string[] scenes = { "Assets/Scenes/DemoScene.unity" };
    static string outPath = System.Environment.GetCommandLineArgs()[1];

    static void BuildWin64()
    {
        BuildPipeline.BuildPlayer(scenes, outPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    static void BuildLinux()
    {
        BuildPipeline.BuildPlayer(scenes, outPath, BuildTarget.StandaloneLinux64, BuildOptions.None);
    }
}

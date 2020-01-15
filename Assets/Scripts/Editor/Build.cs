using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Build : MonoBehaviour
{
    static string[] scenes => EditorBuildSettings.scenes.Select(s => s.path).ToArray();
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

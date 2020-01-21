using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineParser : MonoBehaviour
{
    public static bool SkipMenu;
    public static bool DisableCollisions;
    public static bool LogTimes;
    public static int? QuitAfter;
    public static int? Merchants;
    public static int? Woodcutters;
    public static int? Carpenters;
    public static int? Farmers;

    private void Start()
    {
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            string item = args[i];
            if (item.StartsWith("--"))
            {
                string arg = item.Substring(2);

                if(i < args.Length - 1)
                {
                    if (arg == "quit-after")
                    {
                        QuitAfter = int.Parse(args[i + 1]);
                    }

                    if (arg == "merchants")
                    {
                        Merchants = int.Parse(args[i + 1]);
                    }
                    if (arg == "woodcutters")
                    {
                        Woodcutters = int.Parse(args[i + 1]);
                    }
                    if (arg == "carpenters")
                    {
                        Carpenters = int.Parse(args[i + 1]);
                    }
                    if (arg == "farmers")
                    {
                        Farmers = int.Parse(args[i + 1]);
                    }
                }

                if(arg == "skip-menu")
                {
                    SkipMenu = true;
                }

                if (arg == "disable-collisions")
                {
                    DisableCollisions = true;
                }

                if (arg == "log-times")
                {
                    LogTimes = true;
                }
            }
        }
    }
}

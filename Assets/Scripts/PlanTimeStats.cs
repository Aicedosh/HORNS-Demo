using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanTimeStats : MonoBehaviour
{
    private struct TimeStat
    {
        public double Time { get; }
        public string Name { get; }

        public TimeStat(double time, string name)
        {
            Time = time;
            Name = name;
        }
    }

    private TimeStat min = new TimeStat(double.PositiveInfinity, "");
    private TimeStat max = new TimeStat(double.NegativeInfinity, "");
    private List<TimeStat> all = new List<TimeStat>();

    private object l = new object();

    public void AddTime(double time, string name)
    {
        TimeStat ts = new TimeStat(time, name);
        lock (l)
        {

            all.Add(ts);
        }
        if (ts.Time < min.Time)
        {
            min = ts;
        }

        if(ts.Time > max.Time)
        {
            max = ts;
        }

        PrintStats();
    }

    private void PrintStats()
    {
        lock(l)
        {
            Debug.Log($"[PLAN TIME] Min: {min.Time} ({min.Name}), Max: {max.Time} ({max.Name}), Avg: {all.Select(ts=>ts.Time).Average()}");
        }
    }
}

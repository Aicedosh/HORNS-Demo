using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class TimeLogger
{
    private const string FOLDER = "PlanTime";
    StreamWriter sw;

    public TimeLogger(string filename)
    {
        var di = Directory.CreateDirectory(FOLDER);
        sw = new StreamWriter(Path.Combine(FOLDER, filename));
    }

    public void LogPlanTime(double time)
    {
        sw.WriteLine(time.ToString(CultureInfo.InvariantCulture));
        sw.Flush();
    }

    public void Destroy()
    {
        sw.Close();
    }
}

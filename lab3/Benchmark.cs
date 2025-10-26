using System.Diagnostics;

namespace lab3;

public static class Benchmark
{
    public static void Warmup(Task task, int warmupCount)
    {
        for (var i = 0; i < warmupCount; i++)
        {
            task.Start();
        }
    }

    public static double MeasureDurationInMs(Task task, int repetitionCount)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var stopwatch = new Stopwatch();
        for (var i = 0; i < repetitionCount; i++)
        {
            stopwatch.Start();
            task.Start();
            stopwatch.Stop();
        }

        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
    }
}
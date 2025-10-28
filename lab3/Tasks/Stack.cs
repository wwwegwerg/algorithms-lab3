using System.Diagnostics;
using DataStructures;
using lab3.Charts;

namespace lab3.Tasks;

public static class Stack
{
    public static void Run()
    {
        var cd = BenchStack(5, 5);
        ChartBuilder.Build2DLineChart(cd);
    }

    private static ChartData BenchStack(
        int warmupCount,
        int repetitionCount)
    {
        var dataSize = Helpers.Inputs.Count / 3;

        var results = new List<(string SeriesTitile, IList<DataPoint> Mesuarements)>
        {
            ("Push Heavy", new List<DataPoint>(dataSize)),
            ("Pop Heavy", new List<DataPoint>(dataSize)),
            ("Equally Heavy", new List<DataPoint>(dataSize))
        };

        // Console.WriteLine($"Started at {DateTime.Now.TimeOfDay}");

        Benchmark.Warmup(() => Helpers.ParseData(Helpers.Inputs[0], GetCustomStack()), warmupCount);

        var sw = Stopwatch.StartNew();

        for (var i = 0; i < Helpers.Inputs.Count; i++)
        {
            var input = Helpers.Inputs[i];
            var task = () => Helpers.ParseData(input, GetCustomStack());
            var time = Benchmark.MeasureDurationInMs(task, repetitionCount);
            results[i % 3].Mesuarements.Add(new DataPoint(input.Length, time));
        }

        sw.Stop();
        // Console.WriteLine($"Completed at {DateTime.Now.TimeOfDay}");
        // Console.WriteLine($"Total time: {sw.Elapsed.TotalSeconds}s");

        return new ChartData(
            "stack",
            results,
            "Количество операций",
            "Время (мс)",
            sw.Elapsed.TotalSeconds);
    }

    private static CustomStack<string> GetCustomStack()
    {
        var stack = new CustomStack<string>();
        var stackSize = Helpers.Inputs[^1].Length;
        for (var i = 0; i < stackSize; i++)
        {
            stack.Add(Helpers.Filler[i]);
        }

        return stack;
    }
}
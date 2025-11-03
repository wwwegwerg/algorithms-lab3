using System.Diagnostics;
using DataStructures;
using lab3.Charts;

namespace lab3.Tasks;

public static class Queue
{
    public static void Run()
    {
        var cd = BenchQueue(5, 5);
        ChartBuilder.Build2DLineChart(cd);
    }

    private static ChartData BenchQueue(
        int warmupCount,
        int repetitionCount)
    {
        var dataSize = Helpers.Inputs.Count / 3;

        var results = new List<(string SeriesTitile, IList<DataPoint> Mesuarements)>
        {
            ("Enqueue Heavy C# Default Queue", new List<DataPoint>(dataSize)),
            ("Dequeue Heavy C# Default Queue", new List<DataPoint>(dataSize)),
            ("Equally Heavy C# Default Queue", new List<DataPoint>(dataSize)),
            ("Enqueue Heavy Custom List Queue", new List<DataPoint>(dataSize)),
            ("Dequeue Heavy Custom List Queue", new List<DataPoint>(dataSize)),
            ("Equally Heavy Custom List Queue", new List<DataPoint>(dataSize))
        };

        // Console.WriteLine($"Started at {DateTime.Now.TimeOfDay}");

        Benchmark.Warmup(() => Helpers.ParseData(Helpers.Inputs[0].Values, GetCustomQueue()), warmupCount);

        var sw = Stopwatch.StartNew();

        for (var i = 0; i < Helpers.Inputs.Count; i++)
        {
            var input = Helpers.Inputs[i];
            var idx = input.Preset switch
            {
                "add-heavy" => 0,
                "remove-heavy" => 1,
                "1:1" => 2,
            };

            var customQueueTask = () => Helpers.ParseData(input.Values, GetCustomQueue());
            var customQueueTime = Benchmark.MeasureDurationInMs(customQueueTask, repetitionCount);
            results[idx].Mesuarements.Add(new DataPoint(input.Values.Length, customQueueTime));

            var customListQueueTask = () => Helpers.ParseData(input.Values, GetCustomListQueue());
            var customListQueueTime = Benchmark.MeasureDurationInMs(customListQueueTask, repetitionCount);
            results[idx + 3].Mesuarements.Add(new DataPoint(input.Values.Length, customListQueueTime));
        }

        sw.Stop();
        // Console.WriteLine($"Completed at {DateTime.Now.TimeOfDay}");
        // Console.WriteLine($"Total time: {sw.Elapsed.TotalSeconds}s");

        return new ChartData(
            "queue",
            results,
            "Количество операций",
            "Время (мс)",
            sw.Elapsed.TotalSeconds);
    }

    private static CustomQueue<string> GetCustomQueue()
    {
        var queue = new CustomQueue<string>(false);
        for (var i = 0; i < Helpers.StructSize; i++)
        {
            queue.Enqueue(Helpers.Filler[i]);
        }

        return queue;
    }

    private static CustomListQueue<string> GetCustomListQueue()
    {
        var queue = new CustomListQueue<string>(false);
        for (var i = 0; i < Helpers.StructSize; i++)
        {
            queue.Enqueue(Helpers.Filler[i]);
        }

        return queue;
    }
}
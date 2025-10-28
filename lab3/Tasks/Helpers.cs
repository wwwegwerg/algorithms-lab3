using System.Diagnostics;
using DataStructures;
using lab3.Charts;

namespace lab3.Tasks;

public static class Helpers
{
    private static readonly List<string[]> Inputs = ReadData("input.txt");
    private static readonly string[] Filler = ReadData("filler.txt")[0];

    public static List<string[]> ReadData(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var result = new List<string[]>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var tokens = line.Split(
                [' ', '\t'],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );
            result.Add(tokens);
        }

        return result;
    }

    public static void ParseData(string[] data, IDataStructure<string> ds, bool showOutput = false)
    {
        foreach (var line in data)
        {
            switch (line[0])
            {
                case '1':
                {
                    var value = line.Split(',')[1];
                    ds.Add(value);
                    if (showOutput) Console.WriteLine($"added value: {value}");
                    break;
                }
                case '2':
                {
                    var (status, value) = ds.Remove();
                    var text = status ? value : "empty";
                    if (showOutput) Console.WriteLine($"removed value: {text}");
                    break;
                }
                case '3':
                {
                    var (status, value) = ds.Peek();
                    var text = status ? value : "empty";
                    if (showOutput) Console.WriteLine($"peeked value: {text}");
                    break;
                }
                case '4':
                {
                    var isEmpty = ds.IsEmpty;
                    if (showOutput) Console.WriteLine($"isEmpty: {isEmpty}");
                    break;
                }
                case '5':
                {
                    ds.Print();
                    break;
                }
            }
        }
    }

    public static string[][] BuildPowerOfTwoPrefixes(string[] data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        if (data.Length < 2) throw new ArgumentException("data length is less than 2.", nameof(data));

        var levels = (int)Math.Floor(Math.Log2(data.Length));

        var result = new string[levels][];

        var current = new string[data.Length];
        Array.Copy(data, 0, current, 0, data.Length);

        var len = 2;
        for (var i = 0; i < levels; i++)
        {
            var arr = new string[len];
            Array.Copy(current, 0, arr, 0, len);
            result[i] = arr;
            len <<= 1;
        }

        return result;
    }

    public static ChartData BenchStack(
        int warmupCount,
        int repetitionCount)
    {
        var dataSize = Inputs.Count / 3;

        var results = new List<(string SeriesTitile, IList<DataPoint> Mesuarements)>
        {
            ("Push Heavy", new List<DataPoint>(dataSize)),
            ("Pop Heavy", new List<DataPoint>(dataSize)),
            ("Equally Heavy", new List<DataPoint>(dataSize))
        };

        // Console.WriteLine($"Started at {DateTime.Now.TimeOfDay}");

        Benchmark.Warmup(() => ParseData(Inputs[0], GetCustomStack()), warmupCount);

        var sw = Stopwatch.StartNew();

        for (var i = 0; i < Inputs.Count; i++)
        {
            var input = Inputs[i];
            var task = () => ParseData(input, GetCustomStack());
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
        var stackSize = Inputs[^1].Length;
        for (var i = 0; i < stackSize; i++)
        {
            stack.Add(Filler[i]);
        }

        return stack;
    }

    public static ChartData BenchQueue(
        int warmupCount,
        int repetitionCount)
    {
        var dataSize = Inputs.Count / 3;

        var results = new List<(string SeriesTitile, IList<DataPoint> Mesuarements)>
        {
            ("Push Heavy Custom Queue", new List<DataPoint>(dataSize)),
            ("Pop Heavy Custom Queue", new List<DataPoint>(dataSize)),
            ("Equally Heavy Custom Queue", new List<DataPoint>(dataSize)),
            ("Push Heavy C# Default Queue", new List<DataPoint>(dataSize)),
            ("Pop Heavy C# Default Queue", new List<DataPoint>(dataSize)),
            ("Equally Heavy C# Default Queue", new List<DataPoint>(dataSize))
        };

        // Console.WriteLine($"Started at {DateTime.Now.TimeOfDay}");

        Benchmark.Warmup(() => ParseData(Inputs[0], GetCustomStack()), warmupCount);

        var sw = Stopwatch.StartNew();

        for (var i = 0; i < Inputs.Count; i++)
        {
            var input = Inputs[i];

            var customQueueTask = () => ParseData(input, GetCustomQueue());
            var customQueueTime = Benchmark.MeasureDurationInMs(customQueueTask, repetitionCount);
            results[i % 3].Mesuarements.Add(new DataPoint(input.Length, customQueueTime));

            var customListQueueTask = () => ParseData(input, GetCustomListQueue());
            var customListQueueTime = Benchmark.MeasureDurationInMs(customListQueueTask, repetitionCount);
            results[(i % 3) + 3].Mesuarements.Add(new DataPoint(input.Length, customListQueueTime));
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
        var queue = new CustomQueue<string>();
        var stackSize = Inputs[^1].Length;
        for (var i = 0; i < stackSize; i++)
        {
            queue.Add(Filler[i]);
        }

        return queue;
    }

    private static CustomListQueue<string> GetCustomListQueue()
    {
        var queue = new CustomListQueue<string>();
        var stackSize = Inputs[^1].Length;
        for (var i = 0; i < stackSize; i++)
        {
            queue.Add(Filler[i]);
        }

        return queue;
    }
}
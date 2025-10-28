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

    public static ChartData Build1DTime(
        string title,
        string xLabel,
        string yLabel,
        int warmupCount,
        int repetitionsCount,
        Func<string[], Action> taskFactory)
    {
        var dataSize = Inputs.Count / 3;

        var pushHeavyTimes = new List<DataPoint>(dataSize);
        var popHeavyTimes = new List<DataPoint>(dataSize);
        var equallyHeavyTimes = new List<DataPoint>(dataSize);

        Console.WriteLine($"Started at {DateTime.Now.TimeOfDay}");
        var sw = Stopwatch.StartNew();

        Benchmark.Warmup(taskFactory(Inputs[0]), warmupCount);

        for (var i = 0; i < Inputs.Count; i += 3)
        {
            var pushHeavyInput = Inputs[i];
            var popHeavyInput = Inputs[i + 1];
            var equallyHeavyInput = Inputs[i + 2];

            var pushHeavyTask = taskFactory(pushHeavyInput);
            var popHeavyTask = taskFactory(popHeavyInput);
            var equallyHeavyTask = taskFactory(equallyHeavyInput);

            var pushHeavyTime = Benchmark.MeasureDurationInMs(pushHeavyTask, repetitionsCount);
            var popHeavyTime = Benchmark.MeasureDurationInMs(popHeavyTask, repetitionsCount);
            var equallyHeavyTime = Benchmark.MeasureDurationInMs(equallyHeavyTask, repetitionsCount);

            pushHeavyTimes.Add(new DataPoint(pushHeavyInput.Length, pushHeavyTime));
            popHeavyTimes.Add(new DataPoint(popHeavyInput.Length, popHeavyTime));
            equallyHeavyTimes.Add(new DataPoint(equallyHeavyInput.Length, equallyHeavyTime));
        }

        sw.Stop();
        Console.WriteLine($"Completed at {DateTime.Now.TimeOfDay}");
        Console.WriteLine($"Total time: {sw.Elapsed.TotalSeconds}s");
        return new ChartData(title,
            pushHeavyTimes,
            popHeavyTimes,
            equallyHeavyTimes,
            xLabel,
            yLabel,
            sw.Elapsed.TotalSeconds);
    }
}
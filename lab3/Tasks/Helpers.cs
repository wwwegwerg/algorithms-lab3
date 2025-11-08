using DataStructures;

namespace lab3.Tasks;

public static class Helpers
{
    private const int N = 17;

    public static readonly List<(string Preset, string[] Values)> Inputs = ReadCsvData("input.csv")
        .Where(x => x.Key <= N)
        .Select(x => (x.Preset, x.Values))
        .ToList();

    public static readonly int StructSize = Inputs[^1].Values.Length;

    public static readonly string[] Filler = ReadTxtData("filler.txt")[0];

    private static List<string[]> ReadTxtData(string filePath)
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

        Console.WriteLine($"Файл {filePath} успешно прочитан. Считано {result.Count} строк.");
        return result;
    }

    private static List<(int Key, string Preset, string[] Values)> ReadCsvData(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var result = new List<(int Key, string Preset, string[] Values)>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var columns = line.Split(
                ";",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );

            var key = int.Parse(columns[0]);
            var preset = columns[1];
            var values = columns[2].Split(
                [' ', '\t'],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );
            result.Add((key, preset, values));
        }

        Console.WriteLine($"Файл {filePath} успешно прочитан. Считано {result.Count} строк.");
        return result;
    }

    public static void ParseData(string[] data, CustomStack<string> ds, bool showOutput = false)
    {
        var originalOutputState = ds.ShowOutput;
        ds.ShowOutput = false;
        foreach (var line in data)
        {
            switch (line[0])
            {
                case '1':
                {
                    var result = line.Split(',')[1];
                    ds.Push(result);
                    if (showOutput) Console.WriteLine($"added value: {result}");
                    break;
                }
                case '2':
                {
                    var result = ds.Pop();
                    if (showOutput) Console.WriteLine($"removed value: {result}");
                    break;
                }
                case '3':
                {
                    var result = ds.Top();
                    if (showOutput) Console.WriteLine($"peeked value: {result}");
                    break;
                }
                case '4':
                {
                    var result = ds.IsEmpty;
                    if (showOutput) Console.WriteLine($"isEmpty: {result}");
                    break;
                }
                case '5':
                {
                    ds.Print();
                    break;
                }
            }
        }

        ds.ShowOutput = originalOutputState;
    }

    public static void ParseData(string[] data, QueueWrapper<string> ds, bool showOutput = false)
    {
        var originalOutputState = ds.ShowOutput;
        ds.ShowOutput = false;
        foreach (var line in data)
        {
            switch (line[0])
            {
                case '1':
                {
                    var result = line.Split(',')[1];
                    ds.Enqueue(result);
                    if (showOutput) Console.WriteLine($"added value: {result}");
                    break;
                }
                case '2':
                {
                    var result = ds.Dequeue();
                    if (showOutput) Console.WriteLine($"removed value: {result}");
                    break;
                }
                case '3':
                {
                    var result = ds.Peek();
                    if (showOutput) Console.WriteLine($"peeked value: {result}");
                    break;
                }
                case '4':
                {
                    var result = ds.IsEmpty;
                    if (showOutput) Console.WriteLine($"isEmpty: {result}");
                    break;
                }
                case '5':
                {
                    ds.Print();
                    break;
                }
            }
        }

        ds.ShowOutput = originalOutputState;
    }

    public static void ParseData(string[] data, CustomQueue<string> ds, bool showOutput = false)
    {
        var originalOutputState = ds.ShowOutput;
        ds.ShowOutput = false;
        foreach (var line in data)
        {
            switch (line[0])
            {
                case '1':
                {
                    var result = line.Split(',')[1];
                    ds.Enqueue(result);
                    if (showOutput) Console.WriteLine($"added value: {result}");
                    break;
                }
                case '2':
                {
                    var result = ds.Dequeue();
                    if (showOutput) Console.WriteLine($"removed value: {result}");
                    break;
                }
                case '3':
                {
                    var result = ds.Peek();
                    if (showOutput) Console.WriteLine($"peeked value: {result}");
                    break;
                }
                case '4':
                {
                    var result = ds.IsEmpty;
                    if (showOutput) Console.WriteLine($"isEmpty: {result}");
                    break;
                }
                case '5':
                {
                    ds.Print();
                    break;
                }
            }
        }

        ds.ShowOutput = originalOutputState;
    }
}
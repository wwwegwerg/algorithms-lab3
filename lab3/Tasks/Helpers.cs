using DataStructures;

namespace lab3.Tasks;

public static class Helpers
{
    public static readonly List<string[]> Inputs = ReadData("input.txt");
    public static readonly string[] Filler = ReadData("filler.txt")[0];

    private static List<string[]> ReadData(string filePath)
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
        Console.WriteLine($"Файл {filePath} успешно прочитан.");

        return result;
    }

    public static void ParseData(string[] data, IDataStructure<string> ds, bool showOutput = false)
    {
        var originalOutputState = ds.ShowOutput;
        ds.ShowOutput = false;
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
        ds.ShowOutput = originalOutputState;
    }
}
using DataStructures;

namespace lab3;

public static class Parser
{
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

    public static void ParseData(string[] data, IDataStructure<string> ds)
    {
        foreach (var line in data)
        {
            switch (line[0])
            {
                case '1':
                {
                    var value = line.Split(',')[1];
                    ds.Add(value);
                    Console.WriteLine($"added value: {value}");
                    break;
                }
                case '2':
                {
                    var value = ds.Remove();
                    Console.WriteLine($"removed value: {value}");
                    break;
                }
                case '3':
                {
                    var (status, value) = ds.Peek();
                    var text = status ? value : "empty";
                    Console.WriteLine($"peeked value: {text}");
                    break;
                }
                case '4':
                {
                    var isEmpty = ds.IsEmpty;
                    Console.WriteLine($"isEmpty: {isEmpty}");
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
}
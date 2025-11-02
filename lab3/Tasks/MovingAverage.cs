using DataStructures;

namespace lab3.Tasks;

public static class MovingAverage
{
    public static void Run()
    {
        Console.WriteLine("Введите набор чисел через пробел");
        var input = Console.ReadLine()?.Trim().ToLower();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введите выражение");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        var tokens = input.Split(
            [' ', '\t'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        ).Select(double.Parse).ToList();

        Console.WriteLine("Введите ширину окна");
        input = Console.ReadLine()?.Trim().ToLower();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введите число");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        var result = Calculate(tokens, int.Parse(input));
        Console.WriteLine("Результат: " + string.Join(", ", result));
    }

    private static List<double> Calculate(IList<double> data, int windowSize)
    {
        if (windowSize <= 0)
            throw new ArgumentException("Размер окна должен быть больше 0");
        if (data == null || data.Count < windowSize)
            throw new ArgumentException("Длина данных должна быть не меньше размера окна");

        var window = new CustomListQueue<double>(true);
        var result = new List<double>();
        double sum = 0;

        foreach (var value in data)
        {
            window.Enqueue(value);
            sum += value;

            if (window.Count > windowSize)
                sum -= window.Dequeue();

            if (window.Count == windowSize)
                result.Add(sum / windowSize);
        }

        return result;
    }
}
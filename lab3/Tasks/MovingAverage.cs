using DataStructures;

namespace lab3.Tasks;

public static class MovingAverage
{
    public static void Run()
    {
        Console.WriteLine("Введите набор чисел через пробел");
        var input = Console.ReadLine()?.Trim().ToLower();
        Console.WriteLine("Введите ширину окна");
        var window = Console.ReadLine()?.Trim().ToLower();
        List<double> result;
        try
        {
            var parsed = input
                .Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(double.Parse)
                .ToList();
            result = Calculate(parsed, int.Parse(window));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }

        Console.WriteLine("Результат: " + string.Join(", ", result));
    }

    private static List<double> Calculate(IList<double> data, int windowSize)
    {
        if (windowSize <= 0)
            throw new ArgumentException("Размер окна должен быть больше 0");
        if (data == null || data.Count < windowSize)
            throw new ArgumentException("Длина данных должна быть не меньше размера окна");

        var window = new CustomQueue<double>(true);
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
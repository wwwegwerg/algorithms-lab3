namespace lab3.Tasks;

public static class Kadane
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

        var result = Calculate(tokens);
        Console.WriteLine("Результат: " + result);
    }

    private static double Calculate(IList<double> arr)
    {
        var maxSoFar = arr[0]; // Максимальная сумма на данный момент
        var maxEndingHere = arr[0]; // Максимальная сумма, заканчивающаяся в текущей позиции

        for (var i = 1; i < arr.Count; i++)
        {
            // Решаем — добавлять ли текущий элемент к предыдущей сумме, или начинать новую
            maxEndingHere = Math.Max(arr[i], maxEndingHere + arr[i]);

            // Обновляем глобальный максимум
            maxSoFar = Math.Max(maxSoFar, maxEndingHere);
        }

        return maxSoFar;
    }
}
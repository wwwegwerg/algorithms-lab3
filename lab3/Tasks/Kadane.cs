namespace lab3.Tasks;

public static class Kadane {
    public static void Run() {
        Console.WriteLine("Введите набор чисел через пробел");
        var input = Console.ReadLine()?.Trim().ToLower();
        (double[] BestSegment, double BestSum) result;
        try {
            var parsed = input
                .Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(double.Parse)
                .ToList();
            result = Calculate(parsed);
        } catch (Exception e) {
            Console.WriteLine(e);
            return;
        }

        Console.WriteLine($"Результат: {result.BestSum}; {string.Join(' ', result.BestSegment)}");
    }

    private static (double[], double) Calculate(IList<double> numbers) {
        var bestSum = numbers[0];
        var currentSum = numbers[0];

        var bestStart = 0;
        var bestEnd = 0;
        var currentStart = 0;

        for (var i = 1; i < numbers.Count; i++) {
            if (numbers[i] > currentSum + numbers[i]) {
                currentSum = numbers[i];
                currentStart = i;
            } else {
                currentSum += numbers[i];
            }

            if (!(currentSum > bestSum)) {
                continue;
            }

            bestSum = currentSum;
            bestStart = currentStart;
            bestEnd = i;
        }

        var bestSegment = numbers.Skip(bestStart).Take(bestEnd - bestStart + 1).ToArray();
        return (bestSegment, bestSum);
    }
}
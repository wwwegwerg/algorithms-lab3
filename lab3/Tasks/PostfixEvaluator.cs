using System.Globalization;
using DataStructures;

namespace lab3.Tasks;

public static class PostfixEvaluator
{
    public static void Run()
    {
        Console.WriteLine("Введите выражение в постфиксной форме (каждый символ через пробел)");
        var input = Console.ReadLine()?.Trim().ToLower();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введите выражение");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        var result = Evaluate(input);
        Console.WriteLine("Результат: " + result);
    }

    private static readonly Dictionary<string, Func<double, double>> UnaryOperators = new()
    {
        ["ln"] = a => a <= 0
            ? throw new ArithmeticException("ln определён только для a > 0.")
            : Math.Log(a),

        ["cos"] = Math.Cos,
        ["sin"] = Math.Sin,
        ["sqrt"] = a => a < 0
            ? throw new ArithmeticException("sqrt от отрицательного числа.")
            : Math.Sqrt(a),
    };

    private static readonly Dictionary<string, Func<double, double, double>> BinaryOperators = new()
    {
        ["+"] = (a, b) => a + b,
        ["-"] = (a, b) => a - b,
        ["*"] = (a, b) => a * b,
        ["/"] = (a, b) => b == 0
            ? throw new DivideByZeroException("Деление на ноль.")
            : a / b,
        ["^"] = Math.Pow,
    };

    private static double Evaluate(string expression)
    {
        var stack = new CustomStack<double>(true);
        var tokens = expression.Split(
            [' ', '\t'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );

        foreach (var token in tokens)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            {
                stack.Push(number);
                continue;
            }

            if (UnaryOperators.TryGetValue(token, out var unary))
            {
                if (stack.Count < 1)
                    throw new InvalidOperationException($"Недостаточно операндов для унарной операции '{token}'.");

                var a = stack.Pop();
                var res = unary(a);
                stack.Push(res);
                continue;
            }

            if (BinaryOperators.TryGetValue(token, out var binary))
            {
                if (stack.Count < 2)
                    throw new InvalidOperationException($"Недостаточно операндов для бинарной операции '{token}'.");

                var b = stack.Pop();
                var a = stack.Pop();
                var res = binary(a, b);
                stack.Push(res);
                continue;
            }

            throw new ArgumentException($"Неизвестный оператор: {token}");
        }

        if (stack.Count != 1)
            throw new InvalidOperationException("Ошибка в выражении.");
        return stack.Pop();
    }
}
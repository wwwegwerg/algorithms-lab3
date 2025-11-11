using System.Globalization;
using DataStructures;

namespace lab3.Tasks;

public static class InfixToPostfixConverter {
    public static void Run() {
        Console.WriteLine("Введите выражение в инфиксной форме");
        var input = Console.ReadLine()?.Trim().ToLower();
        List<string> result;
        try {
            result = Convert(input);
        } catch (Exception e) {
            Console.WriteLine(e);
            return;
        }

        Console.WriteLine("Результат: " + string.Join(" ", result));
    }

    private static readonly Dictionary<string, int> Precedence = new()
    {
        { "ln", 5 },
        { "cos", 5 },
        { "sin", 5 },
        { "sqrt", 5 },
        { "^", 4 },
        { "*", 3 },
        { ":", 3 },
        { "/", 3 },
        { "+", 2 },
        { "-", 2 },
        { "(", 1 }
    };

    private static bool IsFunction(string token) {
        return token == "ln" || token == "cos" || token == "sin" || token == "sqrt";
    }

    private static bool IsOperator(string token) {
        return "+-*/:^".Contains(token);
    }

    private static List<string> Convert(string expression) {
        var result = new List<string>();
        var stack = new CustomStack<string>(true);

        var tokens = Helpers.Tokenize(expression);

        foreach (var token in tokens) {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _)) {
                result.Add(token);
            } else if (IsFunction(token)) {
                stack.Push(token);
            } else if (IsOperator(token)) {
                while (!stack.IsEmpty && IsOperator(stack.Top()!) &&
                       Precedence[stack.Top()!] >= Precedence[token]) {
                    result.Add(stack.Pop()!);
                }

                stack.Push(token);
            } else if (token == "(") {
                stack.Push(token);
            } else if (token == ")") {
                while (!stack.IsEmpty && stack.Top() != "(") {
                    result.Add(stack.Pop()!);
                }

                stack.Pop(); // удаляем "("

                if (!stack.IsEmpty && IsFunction(stack.Top()!)) {
                    result.Add(stack.Pop()!);
                }
            }
        }

        while (!stack.IsEmpty) {
            result.Add(stack.Pop()!);
        }

        return result;
    }
}
using System.Globalization;
using DataStructures;

namespace lab3.Tasks;

public static class InfixToPostfixConverter
{
    public static void Run()
    {
        Console.WriteLine("Введите выражение в инфиксной форме (каждый символ через пробел)");
        var input = Console.ReadLine()?.Trim().ToLower();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введите выражение");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        var result = Convert(input);
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

    private static bool IsFunction(string token)
    {
        return token == "ln" || token == "cos" || token == "sin" || token == "sqrt";
    }

    private static bool IsOperator(string token)
    {
        return "+-*/:^".Contains(token);
    }

    private static List<string> Convert(string expression)
    {
        var result = new List<string>();
        var stack = new CustomStack<string>(true);

        var tokens = Tokenize(expression);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                result.Add(token);
            }
            else if (IsFunction(token))
            {
                stack.Push(token);
            }
            else if (IsOperator(token))
            {
                while (!stack.IsEmpty && IsOperator(stack.Top()!) &&
                       Precedence[stack.Top()!] >= Precedence[token])
                {
                    result.Add(stack.Pop()!);
                }

                stack.Push(token);
            }
            else if (token == "(")
            {
                stack.Push(token);
            }
            else if (token == ")")
            {
                while (!stack.IsEmpty && stack.Top() != "(")
                {
                    result.Add(stack.Pop()!);
                }

                stack.Pop(); // удаляем "("

                if (!stack.IsEmpty && IsFunction(stack.Top()!))
                {
                    result.Add(stack.Pop()!);
                }
            }
        }

        while (!stack.IsEmpty)
        {
            result.Add(stack.Pop()!);
        }

        return result;
    }

    private static string[] Tokenize(string expression)
    {
        var tokens = new List<string>();
        var number = "";
        var func = "";

        for (var i = 0; i < expression.Length; i++)
        {
            var c = expression[i];

            if (char.IsWhiteSpace(c))
                continue;

            if (char.IsLetter(c))
            {
                func += c;
                if (i + 1 == expression.Length || !char.IsLetter(expression[i + 1]))
                {
                    tokens.Add(func);
                    func = "";
                }
            }
            else if (char.IsDigit(c) || c == '.')
            {
                number += c;
                if (i + 1 == expression.Length || (!char.IsDigit(expression[i + 1]) && expression[i + 1] != '.'))
                {
                    tokens.Add(number);
                    number = "";
                }
            }
            else
            {
                tokens.Add(c.ToString());
            }
        }

        return tokens.ToArray();
    }
}
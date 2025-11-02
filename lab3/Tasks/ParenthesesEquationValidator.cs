using DataStructures;

namespace lab3.Tasks;

public static class ParenthesesEquationValidator
{
    public static void Run()
    {
        Console.WriteLine("Введите скобочное выражение");
        var input = Console.ReadLine()?.Trim().ToLower();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введите выражение");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        var result = Validate(input);
        Console.WriteLine("Результат: " + result);
    }

    private static readonly Dictionary<char, char> Pairs = new()
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' }
    };

    private static bool Validate(string expression)
    {
        var stack = new CustomStack<char>(true);
        foreach (var c in expression)
        {
            if (Pairs.ContainsKey(c))
            {
                stack.Push(c);
            }
            else if (Pairs.ContainsValue(c))
            {
                if (stack.Count == 0 || Pairs[stack.Pop().Value] != c)
                {
                    return false;
                }
            }
            else return false;
        }

        return stack.Count == 0;
    }
}
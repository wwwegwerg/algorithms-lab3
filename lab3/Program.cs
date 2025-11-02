namespace lab3;

public class Program
{
    public static void Main()
    {
        // Tasks.Queue.Run();
        // Tasks.Stack.Run();

        const string titleStack = "Часть 1. Стек";
        const string titleQueue = "Часть 2. Очередь";
        const string titleDynamicStructs = "Часть 3. Динамические структуры";
        const string titleLinkedList = "Часть 4. Связный список";
        const string titleBinaryTree = "Часть 5. Бинарное дерево";

        var main = new Menu("Главное меню");
        main.Add(1, titleStack, () => GetStackMenu(titleStack).Run(showBack: true));
        main.Add(2, titleQueue, () => GetQueueMenu(titleQueue).Run(showBack: true));
        main.Add(3, titleDynamicStructs, () => GetDynamicStructsMenu(titleDynamicStructs).Run(showBack: true));
        main.Add(4, titleLinkedList, () => GetLinkedListMenu(titleLinkedList).Run(showBack: true));
        main.Add(5, titleBinaryTree, () => GetBinaryTreeMenu(titleBinaryTree).Run(showBack: true));
        main.Add(0, "Выход", () => Environment.Exit(0));

        main.Run();
    }

    private static Menu GetStackMenu(string title)
    {
        var stackMenu = new Menu(title);
        stackMenu.Add(1, "Графики", () =>
        {
            Tasks.Stack.Run();
            Menu.Pause();
        });
        stackMenu.Add(2, "Вычисление постфиксного выражения", () =>
        {
            Tasks.PostfixEvaluator.Run();
            Menu.Pause();
        });
        stackMenu.Add(3, "Перевод инфиксного выражения в постфиксное", () =>
        {
            Tasks.InfixToPostfixConverter.Run();
            Menu.Pause();
        });

        return stackMenu;
    }

    private static Menu GetQueueMenu(string title)
    {
        var queueMenu = new Menu(title);
        queueMenu.Add(1, "Графики", () =>
        {
            Tasks.Queue.Run();
            Menu.Pause();
        });

        return queueMenu;
    }

    private static Menu GetDynamicStructsMenu(string title)
    {
        var dynamicStructsMenu = new Menu(title);
        dynamicStructsMenu.Add(1, "Список: алгоритм Кадане", () =>
        {
            Tasks.Kadane.Run();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(2, "Стек: валидация скобочных выражений", () =>
        {
            Tasks.ParenthesesEquationValidator.Run();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(3, "Очередь: алгоритм скользящего среднего", () =>
        {
            Tasks.MovingAverage.Run();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(4, "Дерево: алгоритм Хаффмана", () =>
        {
            Tasks.Huffman.Run();
            Menu.Pause();
        });

        return dynamicStructsMenu;
    }

    private static Menu GetLinkedListMenu(string title)
    {
        var linkedListMenu = new Menu(title);
        linkedListMenu.Add(1, "Список", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return linkedListMenu;
    }

    private static Menu GetBinaryTreeMenu(string title)
    {
        var linkedListMenu = new Menu(title);
        linkedListMenu.Add(1, "Поиск в ширину", () =>
        {
            Tasks.BFS.Run();
            Menu.Pause();
        });
        linkedListMenu.Add(2, "Поиск в глубину", () =>
        {
            Tasks.DFS.Run();
            Menu.Pause();
        });

        return linkedListMenu;
    }
}
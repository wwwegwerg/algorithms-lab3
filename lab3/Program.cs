using lab3.Tasks;

namespace lab3;

public class Program
{
    public static void Main()
    {
        var main = new Menu("Главное меню");
        main.Add(1, "Часть 1. Стек", () => GetStackMenu().Run(showBack: true));
        main.Add(2, "Часть 2. Очередь", () => GetQueueMenu().Run(showBack: true));
        main.Add(3, "Часть 3. Динамические структуры", () => GetDynamicStructsMenu().Run(showBack: true));
        main.Add(4, "Часть 4. Связный список", () => GetLinkedListMenu().Run(showBack: true));
        main.Add(5, "Дерево", () => GetLinkedListMenu().Run(showBack: true));
        main.Add(0, "Выход", () => Environment.Exit(0));

        main.Run();
    }

    private static Menu GetStackMenu()
    {
        var stackMenu = new Menu("Часть 1. Стек");
        stackMenu.Add(1, "Графики", () =>
        {
            Taskss.Stack();
            Menu.Pause();
        });
        stackMenu.Add(2, "Вычисление постфиксного выражения", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        stackMenu.Add(3, "Перевод инфиксного выражения в постфиксное", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return stackMenu;
    }

    private static Menu GetQueueMenu()
    {
        var queueMenu = new Menu("Часть 2. Очередь");
        queueMenu.Add(1, "Графики", () =>
        {
            Taskss.Queue();
            Menu.Pause();
        });

        return queueMenu;
    }

    private static Menu GetDynamicStructsMenu()
    {
        var dynamicStructsMenu = new Menu("Часть 3. Динамические структуры");
        dynamicStructsMenu.Add(1, "Список: алгоритм Кадане", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(2, "Стек: валидация скобочных выражений", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(3, "Очередь: скользящее среднее", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(4, "Деревья: алгоритм Хаффмана", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return dynamicStructsMenu;
    }

    private static Menu GetLinkedListMenu()
    {
        var linkedListMenu = new Menu("Часть 4. Связный список");
        linkedListMenu.Add(1, "Список", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return linkedListMenu;
    }
}
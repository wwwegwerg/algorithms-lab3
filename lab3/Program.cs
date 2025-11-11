namespace lab3;

public class Program {
    public static void Main() {
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

    private static Menu GetStackMenu(string title) {
        var stackMenu = new Menu(title);
        stackMenu.Add(1, "Графики", () => {
            Tasks.Stack.Run();
            Menu.Pause();
        });
        stackMenu.Add(2, "Вычисление постфиксного выражения", () => {
            Tasks.PostfixEvaluator.Run();
            Menu.Pause();
        });
        stackMenu.Add(3, "Перевод инфиксного выражения в постфиксное", () => {
            Tasks.InfixToPostfixConverter.Run();
            Menu.Pause();
        });

        return stackMenu;
    }

    private static Menu GetQueueMenu(string title) {
        var queueMenu = new Menu(title);
        queueMenu.Add(1, "Графики", () => {
            Tasks.Queue.Run();
            Menu.Pause();
        });

        return queueMenu;
    }

    private static Menu GetDynamicStructsMenu(string title) {
        var dynamicStructsMenu = new Menu(title);
        dynamicStructsMenu.Add(1, "Список: алгоритм Кадане", () => {
            Tasks.Kadane.Run();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(2, "Стек: валидация скобочных выражений", () => {
            Tasks.ParenthesesEquationValidator.Run();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(3, "Очередь: алгоритм скользящего среднего", () => {
            Tasks.MovingAverage.Run();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(4, "Дерево: алгоритм поиска пути между двумя узлами", () => {
            Tasks.TreeAlgorithms.Run();
            Menu.Pause();
        });

        return dynamicStructsMenu;
    }

    private static Menu GetLinkedListMenu(string title) {
        var linkedListMenu = new Menu(title);
        linkedListMenu.Add(1, "Задача 1", () => {
            Tasks.LinkedList.RunTask01();
            Menu.Pause();
        });
        linkedListMenu.Add(2, "Задача 2", () => {
            Tasks.LinkedList.RunTask02();
            Menu.Pause();
        });
        linkedListMenu.Add(3, "Задача 3", () => {
            Tasks.LinkedList.RunTask03();
            Menu.Pause();
        });
        linkedListMenu.Add(4, "Задача 4", () => {
            Tasks.LinkedList.RunTask04();
            Menu.Pause();
        });
        linkedListMenu.Add(5, "Задача 5", () => {
            Tasks.LinkedList.RunTask05();
            Menu.Pause();
        });
        linkedListMenu.Add(6, "Задача 6", () => {
            Tasks.LinkedList.RunTask06();
            Menu.Pause();
        });
        linkedListMenu.Add(7, "Задача 7", () => {
            Tasks.LinkedList.RunTask07();
            Menu.Pause();
        });
        linkedListMenu.Add(8, "Задача 8", () => {
            Tasks.LinkedList.RunTask08();
            Menu.Pause();
        });
        linkedListMenu.Add(9, "Задача 9", () => {
            Tasks.LinkedList.RunTask09();
            Menu.Pause();
        });
        linkedListMenu.Add(10, "Задача 10", () => {
            Tasks.LinkedList.RunTask10();
            Menu.Pause();
        });
        linkedListMenu.Add(11, "Задача 11", () => {
            Tasks.LinkedList.RunTask11();
            Menu.Pause();
        });
        linkedListMenu.Add(12, "Задача 12", () => {
            Tasks.LinkedList.RunTask12();
            Menu.Pause();
        });

        return linkedListMenu;
    }

    private static Menu GetBinaryTreeMenu(string title) {
        var linkedListMenu = new Menu(title);
        linkedListMenu.Add(1, "Обход в ширину", () => {
            Tasks.BFS.Run();
            Menu.Pause();
        });
        linkedListMenu.Add(2, "Обход в глубину (прямой / preorder)", () => {
            Tasks.DFS.Run();
            Menu.Pause();
        });

        return linkedListMenu;
    }
}
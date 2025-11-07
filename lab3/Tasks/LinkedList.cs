using DataStructures;

namespace lab3.Tasks;

public static class LinkedList
{
    public static void RunTask01()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3, 4, 5 })
            list.AddLast(x);
        Console.WriteLine(list);
        list.Task01();
        Console.WriteLine("Результат: " + list);
    }

    public static void RunTask02()
    {
        var list = new DoublyLinkedList<string>();
        foreach (var s in new[] { "A", "B", "C", "D" })
            list.AddLast(s);

        Console.WriteLine(list);
        list.Task02a();
        Console.WriteLine("Последний в начало: " + list);

        list.Task02b();
        list.Task02b();
        Console.WriteLine("Первый в конец: " + list);
    }

    public static void RunTask03()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 3, 1, 2, 3, 2, 2, 5 })
            list.AddLast(x);
        Console.WriteLine(list);
        Console.WriteLine("Результат: " + list.Task03());
    }

    public static void RunTask04()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 2, 3, 4, 4, 4, 5 })
            list.AddLast(x);
        Console.WriteLine(list);
        list.Task04();
        Console.WriteLine("Результат: " + list);
    }

    public static void RunTask05()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3, 2 })
            list.AddLast(x);
        Console.WriteLine(list);
        list.Task05(2);
        Console.WriteLine("Вставили после 2: " + list);
    }

    public static void RunTask06()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 2, 4, 5 })
            list.AddLast(x);
        Console.WriteLine(list);
        list.Task06(3);
        Console.WriteLine("Добавили 3: " + list);
        list.Task06(0);
        Console.WriteLine("Добавили 0: " + list);
        list.Task06(10);
        Console.WriteLine("Добавили 10: " + list);
    }

    public static void RunTask07()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 5, 1, 5, 2, 5, 3 })
            list.AddLast(x);
        Console.WriteLine(list);
        var n = list.Task07(5);
        Console.WriteLine($"Удалили 5: {list}");
    }

    public static void RunTask08()
    {
        var list = new DoublyLinkedList<string>();
        foreach (var s in new[] { "B", "C", "D", "C" })
            list.AddLast(s);
        Console.WriteLine(list);
        list.Task08("C", "X");
        Console.WriteLine("Вставили перед C: " + list);
    }

    public static void RunTask09()
    {
        var L = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3 })
            L.AddLast(x);
        Console.WriteLine("Список L: " + L);
        var E = new DoublyLinkedList<int>();
        foreach (var x in new[] { 4, 5, 6 })
            E.AddLast(x);
        Console.WriteLine("Список E: " + E);
        L.Task09(E);
        Console.WriteLine(L);
    }

    public static void RunTask10()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 10, 20, 30, 40, 50, 30, 60, 70 })
            list.AddLast(x);
        Console.WriteLine(list);
        var second = list.Task10(30);
        Console.WriteLine("Разобьем по 30");
        Console.WriteLine(list);
        Console.WriteLine(second);
    }

    public static void RunTask11()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3, 3 })
            list.AddLast(x);
        Console.WriteLine(list);
        list.Task11();
        Console.WriteLine("Результат: " + list);
    }

    public static void RunTask12()
    {
        var list = new DoublyLinkedList<char>();
        foreach (var c in new[] { 'A', 'B', 'C', 'D', 'E' })
            list.AddLast(c);
        Console.WriteLine(list);
        list.Task12(1, 3);
        Console.WriteLine("i=1; j=3\n" + list);
    }
}
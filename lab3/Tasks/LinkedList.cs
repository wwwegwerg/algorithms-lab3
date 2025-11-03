using DataStructures;

namespace lab3.Tasks;

public static class LinkedList
{
    public static void RunTask01()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3, 4, 5 })
            list.AddLast(x);
        list.Task01();
        Console.WriteLine(list); // 5 -> 4 -> 3 -> 2 -> 1 -> null
    }

    public static void RunTask02()
    {
        var list = new DoublyLinkedList<string>();
        foreach (var s in new[] { "A", "B", "C", "D" })
            list.AddLast(s);

        list.Task02a(); // D A B C
        Console.WriteLine(list);

        list.Task02b(); // A B C D
        Console.WriteLine(list);
    }

    public static void RunTask03()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 3, 1, 2, 3, 2, 2, 5 })
            list.AddLast(x);
        Console.WriteLine(list.Task03()); // 4 (1,2,3,5)
    }

    public static void RunTask04()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 2, 3, 4, 4, 4, 5 })
            list.AddLast(x);
        list.Task04(); // остаются: 1,3,5
        Console.WriteLine(list);
    }

    public static void RunTask05()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3 })
            list.AddLast(x);
        // после первого 2 дописать копию всего списка: 1,2,1,2,3,3
        list.Task05(2);
        Console.WriteLine(list);
    }

    public static void RunTask06()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 2, 4, 5 })
            list.AddLast(x);
        list.Task06(3); // 1,2,2,3,4,5
        list.Task06(0); // 0,1,2,2,3,4,5
        list.Task06(10); // 0,1,2,2,3,4,5,10
        Console.WriteLine(list);
    }

    public static void RunTask07()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 5, 1, 5, 2, 5, 3 })
            list.AddLast(x);
        var n = list.Task07(5); // удалит все 5
        Console.WriteLine($"removed={n} -> {list}");
    }

    public static void RunTask08()
    {
        var list = new DoublyLinkedList<string>();
        foreach (var s in new[] { "B", "C", "D" })
            list.AddLast(s);
        list.Task08("C", "X"); // B, X, C, D
        Console.WriteLine(list);
    }

    public static void RunTask09()
    {
        // имитация: в реальности можно считать из файла
        var L = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3 })
            L.AddLast(x);
        var E = new DoublyLinkedList<int>();
        foreach (var x in new[] { 4, 5, 6 })
            E.AddLast(x);
        L.Task09(E); // 1,2,3,4,5,6
        Console.WriteLine(L);
    }

    public static void RunTask10()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 10, 20, 30, 40, 50 })
            list.AddLast(x);
        // split по первому 30: list = 10,20 ; second = 30,40,50
        var second = list.Task10(30);
        Console.WriteLine(list);
        Console.WriteLine(second);
    }

    public static void RunTask11()
    {
        var list = new DoublyLinkedList<int>();
        foreach (var x in new[] { 1, 2, 3 })
            list.AddLast(x);
        list.Task11(); // 1,2,3,1,2,3
        Console.WriteLine(list);
    }

    public static void RunTask12()
    {
        var list = new DoublyLinkedList<char>();
        foreach (var c in new[] { 'A', 'B', 'C', 'D', 'E' })
            list.AddLast(c);
        list.Task12(1, 3); // A D C B E
        Console.WriteLine(list);
    }
}
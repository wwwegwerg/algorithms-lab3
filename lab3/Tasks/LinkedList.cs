using DataStructures;

namespace lab3.Tasks;

public static class LinkedList
{
    // 1) Разворот списка L (обмен значений двумя указателями)
    public static void ReverseInPlace<T>(DoublyLinkedList<T> list)
    {
        if (list.Count <= 1) return;

        var left = list.First!;
        var right = list.Last!;
        var steps = list.Count / 2;

        for (var i = 0; i < steps; i++)
        {
            (left.Value, right.Value) = (right.Value, left.Value);
            left = left.Next!;
            right = right.Previous!;
        }
    }

    // 2) Перенести в начало последний элемент
    public static void MoveLastToFront<T>(DoublyLinkedList<T> list)
    {
        if (list.Count <= 1) return;
        var last = list.Last!;
        list.RemoveNode(last);
        list.AddFirst(last.Value);
    }

    // 2) Перенести в конец первый элемент
    public static void MoveFirstToEnd<T>(DoublyLinkedList<T> list)
    {
        if (list.Count <= 1) return;
        var first = list.First!;
        list.RemoveNode(first);
        list.AddLast(first.Value);
    }

    // 3) Количество различных элементов (int), без HashSet: O(n^2)
    public static int CountDistinct(DoublyLinkedList<int> list)
    {
        var set = new HashSet<int>();
        foreach (var v in list) set.Add(v);
        return set.Count;
    }

    // 4) Удалить из списка L неуникальные элементы (оставить только те, что встречаются ровно 1 раз)
    // Без словарей: два вложенных прохода, затем удаление всех повторов.
    public static void RemoveNonUnique(DoublyLinkedList<int> list)
    {
        // Идём по значениям; для каждого, если встречается !=1, удаляем все его вхождения.
        var freq = new Dictionary<int, int>();
        foreach (var val in list)
            freq[val] = freq.GetValueOrDefault(val, 0) + 1;

        var i = 0;
        var current = list.First;
        while (current != null)
        {
            var next = current.Next;
            if (freq[current.Value] > 1)
                list.RemoveAt(i);
            else
                i++;
            current = next;
        }
    }

    // 5) Вставить список самого в себя сразу после первого вхождения числа x (int)
    // Без буферов: вставляем по одному, читая "оригинальную" часть.
    public static void InsertSelfAfterFirstX(DoublyLinkedList<int> list, int x)
    {
        if (list.Count == 0) return;
        var idx = list.IndexOf(x);
        if (idx == -1) return;

        var originalCount = list.Count;

        for (var k = 0; k < originalCount; k++)
        {
            var value = list.GetNodeAt(k).Value;
            list.InsertAt(idx + 1 + k, value);
        }
    }

    // 6) Вставить элемент E в неубывающий список L так, чтобы порядок сохранился
    public static void InsertSortedNonDecreasing<T>(DoublyLinkedList<T> list, T item, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        var pos = 0;
        var cur = list.First;
        while (cur != null && comparer.Compare(cur.Value!, item) < 0)
        {
            cur = cur.Next;
            pos++;
        }

        if (cur == null) list.AddLast(item);
        else list.InsertBefore(cur, item);
    }

    // 7) Удалить из списка L все элементы, равные E
    public static void RemoveAll<T>(DoublyLinkedList<T> list, T value)
    {
        var eq = EqualityComparer<T>.Default;
        for (var cur = list.First; cur != null;)
        {
            var nxt = cur.Next;
            if (eq.Equals(cur.Value!, value)) list.RemoveNode(cur);
            cur = nxt;
        }
    }

    // 8) Вставить в список L новый элемент F ПЕРЕД первым вхождением элемента E (если E есть)
    public static bool InsertFBeforeFirstE<T>(DoublyLinkedList<T> list, T e, T f)
    {
        for (var cur = list.First; cur != null; cur = cur.Next)
        {
            if (EqualityComparer<T>.Default.Equals(cur.Value!, e))
            {
                list.InsertBefore(cur, f);
                return true;
            }
        }

        return false;
    }

    // 9) Дописать к списку L список E (оба — целые числа)
    public static void AppendList(DoublyLinkedList<int> L, DoublyLinkedList<int> E)
    {
        for (var cur = E.First; cur != null; cur = cur.Next)
            L.AddLast(cur.Value);
    }

    // 10) Разбить список целых на два по первому вхождению числа delim.
    //      L1 — до delim, L2 — после delim. Сам delim исключаем.
    public static void SplitByFirst(DoublyLinkedList<int> source, int delim,
        out DoublyLinkedList<int> L1, out DoublyLinkedList<int> L2)
    {
        L1 = new DoublyLinkedList<int>();
        L2 = new DoublyLinkedList<int>();

        var found = false;
        for (var cur = source.First; cur != null; cur = cur.Next)
        {
            if (!found && cur.Value == delim)
            {
                found = true;
                continue;
            }

            if (!found) L1.AddLast(cur.Value);
            else L2.AddLast(cur.Value);
        }
    }

    // 11) Удвоить список: приписать в конец его собственную копию
    public static void DoubleSelf<T>(DoublyLinkedList<T> list)
    {
        var originalCount = list.Count;
        var cur = list.First;
        for (var i = 0; i < originalCount; i++, cur = cur!.Next)
            list.AddLast(cur!.Value);
    }

    // 12) Поменять местами два элемента списка:
    //     (a) по индексам
    public static bool SwapByIndex<T>(DoublyLinkedList<T> list, int i, int j)
    {
        if (i < 0 || j < 0 || i >= list.Count || j >= list.Count) return false;
        if (i == j) return true;
        var ni = list.GetNodeAt(i);
        var nj = list.GetNodeAt(j);
        (ni.Value, nj.Value) = (nj.Value, ni.Value);
        return true;
    }

    //     (b) по значениям — первые вхождения a и b
    public static bool SwapByValue<T>(DoublyLinkedList<T> list, T a, T b)
    {
        if (EqualityComparer<T>.Default.Equals(a, b)) return true;

        DoublyLinkedListNode<T>? na = null, nb = null;
        for (var cur = list.First; cur != null && (na == null || nb == null); cur = cur.Next)
        {
            if (na == null && EqualityComparer<T>.Default.Equals(cur.Value, a)) na = cur;
            else if (nb == null && EqualityComparer<T>.Default.Equals(cur.Value, b)) nb = cur;
        }

        if (na == null || nb == null) return false;
        (na.Value, nb.Value) = (nb.Value, na.Value);
        return true;
    }
}
using DataStructures;

namespace lab3.Tasks;

public static class DoublyLinkedList
{
    public static void Run()
    {
    }

    // 1) Разворот списка L (обмен значений двумя указателями)
    public static void ReverseInPlace<T>(DoublyLinkedList<T> list)
    {
        if (list.Count <= 1) return;

        var left = list.First!;
        var right = list.Last!;
        int steps = list.Count / 2;

        for (int i = 0; i < steps; i++)
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
        int distinct = 0;
        int i = 0;
        for (var cur = list.First; cur != null; cur = cur.Next, i++)
        {
            bool seenBefore = false;
            int j = 0;
            for (var left = list.First; left != cur; left = left!.Next, j++)
            {
                if (left!.Value == cur.Value)
                {
                    seenBefore = true;
                    break;
                }
            }

            if (!seenBefore) distinct++;
        }

        return distinct;
    }

    // 4) Удалить из списка L неуникальные элементы (оставить только те, что встречаются ровно 1 раз)
    // Без словарей: два вложенных прохода, затем удаление всех повторов.
    public static void RemoveNonUnique(DoublyLinkedList<int> list)
    {
        // Идём по значениям; для каждого, если встречается !=1, удаляем все его вхождения.
        for (var cur = list.First; cur != null;)
        {
            int value = cur.Value;
            int count = 0;

            // Подсчитать количество вхождений value
            for (var probe = list.First; probe != null; probe = probe.Next)
                if (probe.Value == value)
                    count++;

            // Сохраним следующий до возможных удалений
            var nextDistinct = cur.Next;

            if (count != 1)
            {
                // удалить все вхождения value
                for (var probe = list.First; probe != null;)
                {
                    var nxt = probe.Next;
                    if (probe.Value == value) list.RemoveNode(probe);
                    probe = nxt;
                }
            }

            cur = nextDistinct;
            // пропускаем значения, которых уже нет или которые мы уже обработали
            while (cur != null && cur.Value == value) cur = cur.Next;
        }
    }

    // 5) Вставить список самого в себя сразу после первого вхождения числа x (int)
    // Без буферов: вставляем по одному, читая "оригинальную" часть.
    public static void InsertSelfAfterFirstX(DoublyLinkedList<int> list, int x)
    {
        if (list.Count == 0) return;
        int idx = list.IndexOf(x);
        if (idx == -1) return;

        // фиксируем исходную длину — столько элементов надо вставить копией
        int originalCount = list.Count;

        // Вставляем последовательные значения исходной "первой половины"
        // Берём по индексу i (0..originalCount-1) и вставляем после позиции (idx + k)
        // ВАЖНО: брать значение всегда из первых originalCount узлов
        for (int k = 0; k < originalCount; k++)
        {
            // читаем значение из "старого" сегмента (он всё ещё первые originalCount узлов)
            var value = list.GetNodeAt(k).Value;

            // позиция вставки: сразу после x + уже вставленных k элементов
            list.InsertAt(idx + 1 + k, value);
        }
    }

    // 6) Вставить элемент E в неубывающий список L так, чтобы порядок сохранился
    public static void InsertSortedNonDecreasing<T>(DoublyLinkedList<T> list, T item, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        // найти первую позицию, где v >= item
        int pos = 0;
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

        bool found = false;
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
        int originalCount = list.Count;
        // берём значения из первых originalCount узлов и добавляем в конец
        var cur = list.First;
        for (int i = 0; i < originalCount; i++, cur = cur!.Next)
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
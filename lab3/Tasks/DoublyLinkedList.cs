using DataStructures;

namespace lab3.Tasks;

public static class DoublyLinkedList
{
    public static void Run()
    {
    }

    // -----------------------------
    // 1) Развернуть список L
    // -----------------------------
    public static void ReverseInPlace<T>(DoublyLinkedList<T> list)
    {
        var buffer = new List<T>(list.Count);
        foreach (var v in list)
            buffer.Add(v);

        list.Clear();
        for (var i = buffer.Count - 1; i >= 0; i--)
            list.AddLast(buffer[i]);
    }

    // ----------------------------------------------------------
    // 2) Перенести в начало последний элемент / в конец первый
    // ----------------------------------------------------------
    public static void MoveLastToFront<T>(DoublyLinkedList<T> list)
    {
        if (list.Count <= 1) return;
        var val = list.Last!.Value;
        list.RemoveLast();
        list.AddFirst(val);
    }

    public static void MoveFirstToEnd<T>(DoublyLinkedList<T> list)
    {
        if (list.Count <= 1) return;
        var val = list.First!.Value;
        list.RemoveFirst();
        list.AddLast(val);
    }

    // --------------------------------------------
    // 3) Кол-во различных элементов (для int-списка)
    // --------------------------------------------
    public static int CountDistinct(DoublyLinkedList<int> list)
    {
        var set = new HashSet<int>();
        foreach (var v in list) set.Add(v);
        return set.Count;
    }

    // ----------------------------------------------------------
    // 4) Удалить из списка L неуникальные элементы (int-список)
    // ----------------------------------------------------------
    public static void RemoveNonUnique(DoublyLinkedList<int> list)
    {
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

    // --------------------------------------------------------------------
    // 5) Вставка списка самого в себя после первого вхождения числа x (int)
    // --------------------------------------------------------------------
    public static void InsertSelfAfterFirstX(DoublyLinkedList<int> list, int x)
    {
        if (list.Count == 0) return;

        var snapshot = ToList(list);
        var idx = snapshot.FindIndex(v => v == x);
        if (idx == -1) return;

        var result = new List<int>(snapshot.Count * 2);
        for (var i = 0; i < snapshot.Count; i++)
        {
            result.Add(snapshot[i]);
            if (i == idx)
                result.AddRange(snapshot);
        }

        list.Clear();
        foreach (var v in result) list.AddLast(v);
    }

    // ------------------------------------------------------------------------------------
    // 6) Вставить элемент E в неубывающий список L так, чтобы порядок сохранился (generic)
    // ------------------------------------------------------------------------------------
    public static void InsertSortedNonDecreasing<T>(DoublyLinkedList<T> list, T item, IComparer<T>? comparer = null)
    {
        comparer ??= Comparer<T>.Default;

        var result = new DoublyLinkedList<T>();
        var inserted = false;

        foreach (var v in list)
        {
            if (!inserted && comparer.Compare(item, v) <= 0)
            {
                result.AddLast(item);
                inserted = true;
            }

            result.AddLast(v);
        }

        if (!inserted) result.AddLast(item);

        list.Clear();
        foreach (var v in result) list.AddLast(v);
    }

    // -----------------------------------------------------------
    // 7) Удалить из списка L все элементы, равные E. Возврат count
    // -----------------------------------------------------------
    public static int RemoveAll<T>(DoublyLinkedList<T> list, T value)
    {
        var eq = EqualityComparer<T>.Default;
        var removed = 0;
        var keep = new List<T>();

        foreach (var v in list)
        {
            if (eq.Equals(v, value)) removed++;
            else keep.Add(v);
        }

        if (removed > 0)
        {
            list.Clear();
            foreach (var v in keep) list.AddLast(v);
        }

        return removed;
    }

    // ------------------------------------------------------------------
    // 8) Вставить новый элемент F перед первым вхождением элемента E
    //    Возвращает true, если вставка была выполнена
    // ------------------------------------------------------------------
    public static bool InsertBeforeFirst<T>(DoublyLinkedList<T> list, T e, T f)
    {
        var eq = EqualityComparer<T>.Default;
        var result = new DoublyLinkedList<T>();
        var inserted = false;

        foreach (var v in list)
        {
            if (!inserted && eq.Equals(v, e))
            {
                result.AddLast(f);
                inserted = true;
            }

            result.AddLast(v);
        }

        if (!inserted) return false;

        list.Clear();
        foreach (var v in result) list.AddLast(v);
        return true;
    }

    // ---------------------------------------------------------
    // 9) Дописать к списку L список E (оба — списки целых чисел)
    //    Чтение из файла делайте в main; при желании — вспомогалка ниже
    // ---------------------------------------------------------
    public static void AppendList(DoublyLinkedList<int> L, DoublyLinkedList<int> E)
    {
        foreach (var v in E) L.AddLast(v);
    }

    // (опционально) удобная функция чтения int-списка из файла (по одному числу в строке)
    public static DoublyLinkedList<int> ReadIntListFromFile(string path)
    {
        var list = new DoublyLinkedList<int>();
        foreach (var line in File.ReadLines(path))
        {
            if (int.TryParse(line, out var val))
                list.AddLast(val);
        }
        return list;
    }

    // --------------------------------------------------------------------------------------------
    // 10) Разбить список целых на два по первому вхождению заданного числа delim.
    //     Возвращает два новых списка: L1 — до delim, L2 — после delim.
    //     Если delim нет, L2 пуст, L1 = копия исходного. Исходный список не меняется.
    // --------------------------------------------------------------------------------------------
    public static void SplitByFirst(DoublyLinkedList<int> source, int delim,
        out DoublyLinkedList<int> L1, out DoublyLinkedList<int> L2)
    {
        L1 = new DoublyLinkedList<int>();
        L2 = new DoublyLinkedList<int>();
        var found = false;

        foreach (var v in source)
        {
            if (!found && v == delim)
            {
                found = true;
                continue; // сам разделитель не включаем
            }

            if (!found) L1.AddLast(v);
            else L2.AddLast(v);
        }

        if (!found)
        {
            // Нужно, чтобы L1 была копией исходного, а L2 — пустой (уже так).
            L1 = new DoublyLinkedList<int>();
            foreach (var v in source) L1.AddLast(v);
            L2 = new DoublyLinkedList<int>();
        }
    }

    // -------------------------------------------------------
    // 11) Удвоить список: приписать к концу его собственную копию
    // -------------------------------------------------------
    public static void DoubleSelf<T>(DoublyLinkedList<T> list)
    {
        var snapshot = ToList(list);
        foreach (var v in snapshot) list.AddLast(v);
    }

    // -------------------------------------------------------
    // 12) Поменять местами два элемента списка (два варианта)
    //     а) по индексам (0-based)
    //     б) по значениям — первые вхождения a и b
    // -------------------------------------------------------
    public static bool SwapByIndex<T>(DoublyLinkedList<T> list, int i, int j)
    {
        if (i == j) return true;
        if (i < 0 || j < 0 || i >= list.Count || j >= list.Count) return false;
        if (i > j) (i, j) = (j, i);

        var arr = ToList(list);
        (arr[i], arr[j]) = (arr[j], arr[i]);

        list.Clear();
        foreach (var v in arr) list.AddLast(v);
        return true;
    }

    public static bool SwapByValue<T>(DoublyLinkedList<T> list, T a, T b)
    {
        var arr = ToList(list);
        var ia = arr.FindIndex(v => EqualityComparer<T>.Default.Equals(v, a));
        var ib = arr.FindIndex(v => EqualityComparer<T>.Default.Equals(v, b));
        if (ia == -1 || ib == -1) return false;

        (arr[ia], arr[ib]) = (arr[ib], arr[ia]);
        list.Clear();
        foreach (var v in arr) list.AddLast(v);
        return true;
    }

    // ---------------------------------
    // Вспомогательная приватная утилита
    // ---------------------------------
    private static List<T> ToList<T>(DoublyLinkedList<T> list)
    {
        var res = new List<T>(list.Count);
        foreach (var v in list) res.Add(v);
        return res;
    }
}
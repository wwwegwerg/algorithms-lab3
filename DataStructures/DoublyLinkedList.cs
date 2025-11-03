using System.Collections;
using System.Text;

namespace DataStructures;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public ListNode<T>? First { get; private set; }
    public ListNode<T>? Last { get; private set; }
    public int Count { get; private set; }

    public void AddFirst(T value)
    {
        var newNode = new ListNode<T>(value);
        if (First == null)
        {
            First = newNode;
            Last = newNode;
            Count++;
            return;
        }

        newNode.Next = First;
        First.Previous = newNode;
        First = newNode;
        Count++;
    }

    public void AddLast(T value)
    {
        var newNode = new ListNode<T>(value);
        if (First == null)
        {
            First = newNode;
            Last = newNode;
            Count++;
            return;
        }

        newNode.Previous = Last;
        Last!.Next = newNode;
        Last = newNode;
        Count++;
    }

    public T RemoveFirst()
    {
        if (First == null)
        {
            throw new InvalidOperationException("The List is empty");
        }

        T result;
        if (Count == 1)
        {
            var node = First;
            result = node.Value;
            node.Invalidate();
            First = Last = null;
            Count = 0;
            return result;
        }

        var oldFirst = First;
        var newFirst = oldFirst.Next;
        result = oldFirst.Value;
        oldFirst.Invalidate();
        newFirst!.Previous = null;
        First = newFirst;
        Count--;
        return result;
    }

    public T RemoveLast()
    {
        if (First == null)
        {
            throw new InvalidOperationException("The List is empty");
        }

        T result;
        if (Count == 1)
        {
            var node = Last;
            result = node!.Value;
            node.Invalidate();
            First = Last = null;
            Count = 0;
            return result;
        }

        var oldLast = Last;
        var newLast = oldLast!.Previous;
        result = oldLast.Value;
        oldLast.Invalidate();
        newLast!.Next = null;
        Last = newLast;
        Count--;
        return result;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var current = First;
        while (current != null)
        {
            sb.Append(current);
            sb.Append(" -> ");
            current = current.Next;
        }

        sb.Append("null");
        return sb.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = First;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private ListNode<T>? FindFirstNode(T value)
    {
        var cur = First;
        while (cur != null)
        {
            if (EqualityComparer<T>.Default.Equals(cur.Value, value))
                return cur;
            cur = cur.Next;
        }

        return null;
    }

    private void RemoveNode(ListNode<T> node)
    {
        var prev = node.Previous;
        var next = node.Next;

        if (prev != null) prev.Next = next;
        else First = next;
        if (next != null) next.Previous = prev;
        else Last = prev;

        node.Invalidate();
        Count--;
    }

    private void InsertBefore(ListNode<T> where, ListNode<T> newNode)
    {
        var prev = where.Previous;
        newNode.Next = where;
        newNode.Previous = prev;
        where.Previous = newNode;
        if (prev != null) prev.Next = newNode;
        else First = newNode;
        Count++;
    }

    private ListNode<T>? GetNodeAt(int index)
    {
        if (index < 0 || index >= Count) return null;

        // двусторонний проход — выбираем ближе
        if (index <= Count / 2)
        {
            int i = 0;
            var cur = First;
            while (i < index)
            {
                cur = cur!.Next;
                i++;
            }

            return cur;
        }
        else
        {
            int i = Count - 1;
            var cur = Last;
            while (i > index)
            {
                cur = cur!.Previous;
                i--;
            }

            return cur;
        }
    }

    public void Task01()
    {
        if (Count <= 1) return;

        var current = First;
        while (current != null)
        {
            var next = current.Next;
            // меняем местами ссылки
            current.Next = current.Previous;
            current.Previous = next;
            current = next;
        }

        (First, Last) = (Last, First);
    }

    // MoveLastToFront
    public void Task02a()
    {
        if (Count <= 1) return;

        var oldLast = Last!;
        var newLast = oldLast.Previous!;
        // отцепляем oldLast
        newLast.Next = null;
        Last = newLast;

        // вставляем в голову
        oldLast.Previous = null;
        oldLast.Next = First;
        First!.Previous = oldLast;
        First = oldLast;
    }

    // MoveFirstToBack
    public void Task02b()
    {
        if (Count <= 1) return;

        var oldFirst = First!;
        var newFirst = oldFirst.Next!;
        // отцепляем oldFirst
        newFirst.Previous = null;
        First = newFirst;

        // вставляем в хвост
        oldFirst.Next = null;
        oldFirst.Previous = Last;
        Last!.Next = oldFirst;
        Last = oldFirst;
    }

    public int Task03()
    {
        var distinct = 0;
        for (var i = First; i != null; i = i.Next)
        {
            var seenBefore = false;
            for (var j = First; j != i; j = j!.Next)
            {
                if (!EqualityComparer<T>.Default.Equals(j!.Value, i.Value)) continue;
                seenBefore = true;
                break;
            }

            if (!seenBefore) distinct++;
        }

        return distinct;
    }

    public void Task04()
    {
        var current = First;
        while (current != null)
        {
            var value = current.Value;

            // посчитать количество вхождений value
            var occ = 0;
            for (var r = First; r != null; r = r.Next)
                if (EqualityComparer<T>.Default.Equals(r.Value, value))
                    occ++;

            var nextCurrent = current.Next;

            if (occ > 1)
            {
                // удалить все вхождения value
                var r = First;
                while (r != null)
                {
                    var next = r.Next;
                    if (EqualityComparer<T>.Default.Equals(r.Value, value))
                        RemoveNode(r);
                    r = next;
                }
            }

            current = nextCurrent;
        }
    }

    public bool Task05(T x)
    {
        var target = FindFirstNode(x);
        if (target == null) return false;

        if (Count == 0) return true;

        var originalLast = Last!;
        var insertAfter = target;
        var cursor = First;

        while (true)
        {
            var clone = new ListNode<T>(cursor!.Value);

            // вставка после insertAfter
            var after = insertAfter.Next;
            insertAfter.Next = clone;
            clone.Previous = insertAfter;
            clone.Next = after;
            if (after != null) after.Previous = clone;
            else Last = clone;

            insertAfter = clone;
            Count++;

            if (cursor == originalLast) break;
            cursor = cursor.Next;
        }

        return true;
    }

    public void Task06(T value)
    {
        var cmp = Comparer<T>.Default;
        if (First == null)
        {
            AddFirst(value);
            return;
        }

        // если value должен быть в голове
        if (cmp.Compare(value, First!.Value) <= 0)
        {
            var node = new ListNode<T>(value);
            node.Next = First;
            First.Previous = node;
            First = node;
            Count++;
            return;
        }

        // поиск места
        var cur = First;
        while (cur!.Next != null && cmp.Compare(cur.Next.Value, value) < 0)
            cur = cur.Next;

        // вставка после cur
        var n = new ListNode<T>(value);
        var after = cur.Next;
        cur.Next = n;
        n.Previous = cur;
        n.Next = after;
        if (after != null) after.Previous = n;
        else Last = n;
        Count++;
    }

    public int Task07(T value)
    {
        int removed = 0;
        var cur = First;
        while (cur != null)
        {
            var next = cur.Next;
            if (EqualityComparer<T>.Default.Equals(cur.Value, value))
            {
                RemoveNode(cur);
                removed++;
            }

            cur = next;
        }

        return removed;
    }

    public bool Task08(T target, T newValue)
    {
        var node = FindFirstNode(target);
        if (node == null) return false;

        var n = new ListNode<T>(newValue);
        InsertBefore(node, n);
        return true;
    }

    public void Task09(DoublyLinkedList<T> other)
    {
        if (other == null || other.Count == 0) return;

        var cur = other.First;
        while (cur != null)
        {
            AddLast(cur.Value); // клонируем значения
            cur = cur.Next;
        }
    }

    public DoublyLinkedList<T> Task10(T x)
    {
        var node = FindFirstNode(x);
        if (node == null) return new DoublyLinkedList<T>();

        var second = new DoublyLinkedList<T>();

        // второй список начинается с node
        second.First = node;
        second.Last = Last;

        // отцепляем от первого
        var before = node.Previous;
        if (before != null)
        {
            before.Next = null;
            node.Previous = null;
            Last = before;
        }
        else
        {
            // разделение с головы: первый станет пустым
            First = Last = null;
        }

        // посчитать размеры (без доп. структур)
        int secondCount = 0;
        for (var c = second.First; c != null; c = c.Next) secondCount++;
        int firstCount = Count - secondCount;

        second.Count = secondCount;
        Count = firstCount;

        return second;
    }

    public void Task11()
    {
        if (Count == 0) return;

        var originalLast = Last!;
        var insertAfter = originalLast;
        var cur = First;

        while (true)
        {
            var clone = new ListNode<T>(cur!.Value);

            // вставка после insertAfter (в конец текущей цепочки)
            insertAfter.Next = clone;
            clone.Previous = insertAfter;
            insertAfter = clone;
            Count++;

            if (cur == originalLast) break;
            cur = cur.Next;
        }

        Last = insertAfter;
    }

    public void Task12(int i, int j)
    {
        if (i == j) return;
        if (i < 0 || j < 0 || i >= Count || j >= Count)
            throw new ArgumentOutOfRangeException();

        if (i > j) (i, j) = (j, i);

        var a = GetNodeAt(i);
        var b = GetNodeAt(j);
        if (a == null || b == null) throw new InvalidOperationException();

        if (a.Next == b) // соседние
        {
            var pa = a.Previous;
            var nb = b.Next;

            if (pa != null) pa.Next = b;
            else First = b;
            b.Previous = pa;

            b.Next = a;
            a.Previous = b;

            a.Next = nb;
            if (nb != null) nb.Previous = a;
            else Last = a;
            return;
        }

        // несоседние
        var pa2 = a.Previous;
        var na2 = a.Next;
        var pb2 = b.Previous;
        var nb2 = b.Next;

        if (pa2 != null) pa2.Next = b;
        else First = b;
        if (na2 != null) na2.Previous = b;

        if (pb2 != null) pb2.Next = a;
        else First = a;
        if (nb2 != null) nb2.Previous = a;
        else Last = a;

        a.Previous = pb2;
        a.Next = nb2;
        b.Previous = pa2;
        b.Next = na2;
    }
}

public class ListNode<T>
{
    public T Value { get; set; }
    public ListNode<T>? Next { get; internal set; }
    public ListNode<T>? Previous { get; internal set; }

    public ListNode(T value)
    {
        Value = value;
    }

    internal void Invalidate()
    {
        Next = null;
        Previous = null;
    }

    public override string? ToString()
    {
        if (Value == null) return "";
        return Value.ToString();
    }
}
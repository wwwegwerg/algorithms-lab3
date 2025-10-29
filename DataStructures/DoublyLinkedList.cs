using System.Collections;
using System.Text;

namespace DataStructures;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private DoublyLinkedListNode<T>? _head;
    private DoublyLinkedListNode<T>? _tail;
    public DoublyLinkedListNode<T>? First => _head;
    public DoublyLinkedListNode<T>? Last => _tail;
    public int Count { get; private set; }

    public void AddFirst(T? value)
    {
        var newNode = new DoublyLinkedListNode<T>(value);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            Count++;
            return;
        }

        newNode.Next = _head;
        _head.Previous = newNode;
        Count++;
        _head = newNode;
    }

    public void AddLast(T? value)
    {
        var newNode = new DoublyLinkedListNode<T>(value);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            Count++;
            return;
        }

        newNode.Previous = _tail;
        _tail.Next = newNode;
        Count++;
        _tail = newNode;
    }

    public void Clear()
    {
        var current = _head;
        while (current != null)
        {
            var temp = current;
            current = current.Next;
            temp.Invalidate();
        }

        _head = null;
        _tail = null;
        Count = 0;
    }

    public void RemoveFirst()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        if (Count == 1)
        {
            var node = _head;
            _head = _tail = null;
            Count = 0;
            node.Invalidate();
            return;
        }

        var first = _head!;
        var newFirst = first.Next!;
        newFirst.Previous = null;

        _head = newFirst;
        first.Invalidate();
        Count--;
    }

    public void RemoveLast()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        if (Count == 1)
        {
            var node = _head;
            _head = _tail = null;
            Count = 0;
            node.Invalidate();
            return;
        }

        var last = _tail!;
        var newLast = last.Previous!;
        newLast.Next = null;

        _tail = newLast;
        last.Invalidate();
        Count--;
    }

    public DoublyLinkedListNode<T> GetNodeAt(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        // оптимизация: идём с ближайшего конца
        if (index <= Count / 2)
        {
            var cur = _head!;
            for (int i = 0; i < index; i++) cur = cur.Next!;
            return cur;
        }
        else
        {
            var cur = _tail!;
            for (int i = Count - 1; i > index; i--) cur = cur.Previous!;
            return cur;
        }
    }

    // Вставить значение ПОСЛЕ данного узла
    public DoublyLinkedListNode<T> InsertAfter(DoublyLinkedListNode<T> node, T? value)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        var newNode = new DoublyLinkedListNode<T>(value);

        var next = node.Next;
        newNode.Previous = node;
        newNode.Next = next;
        node.Next = newNode;
        if (next != null) next.Previous = newNode;
        else _tail = newNode;

        Count++;
        return newNode;
    }

    // Вставить значение ПЕРЕД данным узлом
    public DoublyLinkedListNode<T> InsertBefore(DoublyLinkedListNode<T> node, T? value)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        var newNode = new DoublyLinkedListNode<T>(value);

        var prev = node.Previous;
        newNode.Next = node;
        newNode.Previous = prev;
        node.Previous = newNode;
        if (prev != null) prev.Next = newNode;
        else _head = newNode;

        Count++;
        return newNode;
    }

    // Удалить КОНКРЕТНЫЙ узел (любой: первый/средний/последний)
    public void RemoveNode(DoublyLinkedListNode<T> node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));

        var prev = node.Previous;
        var next = node.Next;

        if (prev != null) prev.Next = next;
        else _head = next;
        if (next != null) next.Previous = prev;
        else _tail = prev;

        node.Invalidate();
        Count--;
    }

    // Удалить по индексу
    public void RemoveAt(int index) => RemoveNode(GetNodeAt(index));

    // Вставить по индексу: перед элементом с данным индексом (или в конец, если index == Count)
    public void InsertAt(int index, T? value)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index == 0)
        {
            AddFirst(value);
            return;
        }

        if (index == Count)
        {
            AddLast(value);
            return;
        }

        InsertBefore(GetNodeAt(index), value);
    }

    // Поиск индекса первого вхождения (или -1)
    public int IndexOf(T? value)
    {
        var c = EqualityComparer<T?>.Default;
        int i = 0;
        for (var cur = _head; cur != null; cur = cur.Next, i++)
            if (c.Equals(cur.Value, value))
                return i;
        return -1;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var current = _head;
        while (current != null)
        {
            sb.Append(current.Value);
            sb.Append(" -> ");
            current = current.Next;
        }

        sb.Append("null");
        return sb.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;
        while (current != null)
        {
            yield return current.Value!;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class DoublyLinkedListNode<T>(T? value)
{
    public T? Value { get; set; } = value;
    public DoublyLinkedListNode<T>? Next { get; internal set; }
    public DoublyLinkedListNode<T>? Previous { get; internal set; }

    internal void Invalidate()
    {
        Next = null;
        Previous = null;
    }
}
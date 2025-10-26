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

    public bool Contains(T? value)
    {
        return Find(value) != null;
    }

    public DoublyLinkedListNode<T>? Find(T? value)
    {
        var current = _head;
        var c = EqualityComparer<T?>.Default;

        while (current != null)
        {
            if (c.Equals(current.Value, value))
                return current;

            current = current.Next;
        }

        return null;
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
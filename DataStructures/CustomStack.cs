namespace DataStructures;

public class CustomStack<T> : IDataStructure<T>
{
    private readonly DoublyLinkedList<T?> _list = new();
    public int Count => _list.Count;

    public void Add(T? item)
    {
        _list.AddLast(item);
    }

    public (bool success, T? value) Remove()
    {
        if (_list.Last == null)
            return (false, default);
        var result = _list.Last.Value;
        _list.RemoveLast();
        return (true, result);
    }

    public (bool success, T? value) Peek()
    {
        if (_list.Last == null)
            return (false, default);
        return (true, _list.Last.Value);
    }

    public bool IsEmpty => _list.Count == 0;

    public void Print()
    {
        Console.WriteLine(this);
    }

    public override string ToString()
    {
        return "{" + string.Join(", ", _list) + "}";
    }
}
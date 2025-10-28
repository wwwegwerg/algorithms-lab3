namespace DataStructures;

public class CustomListQueue<T> : IDataStructure<T>
{
    private readonly List<T?> _list = new();
    public int Count => _list.Count;

    public void Add(T? item)
    {
        _list.Insert(0, item);
    }

    public (bool success, T? value) Remove()
    {
        if (_list.Count == 0)
            return (false, default);
        var result = _list[^1];
        _list.RemoveAt(_list.Count - 1);
        return (true, result);
    }

    public (bool success, T? value) Peek()
    {
        if (_list.Count == 0)
            return (false, default);
        return (true, _list[^1]);
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
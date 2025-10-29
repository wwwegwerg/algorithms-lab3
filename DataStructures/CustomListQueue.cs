namespace DataStructures;

public class CustomListQueue<T> : IDataStructure<T>
{
    private readonly List<T?> _list = new();

    public CustomListQueue(bool showOutput)
    {
        ShowOutput = showOutput;
    }

    public bool ShowOutput { get; set; }
    public int Count => _list.Count;

    public void Add(T? item)
    {
        _list.Insert(0, item);
        if (ShowOutput) Print();
    }

    public (bool Success, T? Value) Remove()
    {
        if (_list.Count == 0)
        {
            if (ShowOutput) Print();
            return (false, default);
        }
        var result = _list[^1];
        _list.RemoveAt(_list.Count - 1);
        if (ShowOutput) Print();
        return (true, result);
    }

    public (bool Success, T? Value) Peek()
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
        return "{ " + string.Join(", ", _list) + " }";
    }
}
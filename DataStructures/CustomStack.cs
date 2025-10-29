namespace DataStructures;

public class CustomStack<T> : IDataStructure<T>
{
    private readonly DoublyLinkedList<T?> _list = new();

    public CustomStack(bool showOutput)
    {
        ShowOutput = showOutput;
    }

    public bool ShowOutput { get; set; }
    public int Count => _list.Count;

    public void Add(T? item)
    {
        _list.AddLast(item);
        if (ShowOutput) Print();
    }

    public (bool Success, T? Value) Remove()
    {
        if (_list.Last == null)
        {
            if (ShowOutput) Print();
            return (false, default);
        }

        var result = _list.Last.Value;
        _list.RemoveLast();
        if (ShowOutput) Print();
        return (true, result);
    }

    public (bool Success, T? Value) Peek()
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
        return "{ " + string.Join(", ", _list) + " }";
    }
}
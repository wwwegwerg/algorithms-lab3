namespace DataStructures;

public class CustomListQueue<T>
{
    private readonly List<T?> _list = [];

    public CustomListQueue(bool showOutput)
    {
        ShowOutput = showOutput;
    }

    public bool ShowOutput { get; set; }
    public int Count => _list.Count;

    public void Enqueue(T? item)
    {
        _list.Add(item);
        if (ShowOutput) Print();
    }

    public (bool Success, T? Value) Dequeue()
    {
        if (_list.Count == 0)
        {
            if (ShowOutput) Print();
            return (false, default);
        }

        var result = _list[0];
        _list.RemoveAt(0);
        if (ShowOutput) Print();
        return (true, result);
    }

    public (bool Success, T? Value) Peek()
    {
        if (_list.Count == 0)
            return (false, default);
        return (true, _list[0]);
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
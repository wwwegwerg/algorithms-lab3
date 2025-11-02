namespace DataStructures;

public class CustomStack<T>
{
    private readonly DoublyLinkedList<T?> _list = new();

    public CustomStack(bool showOutput)
    {
        ShowOutput = showOutput;
    }

    public bool ShowOutput { get; set; }
    public int Count => _list.Count;

    public void Push(T? item)
    {
        _list.AddFirst(item);
        if (ShowOutput) Print();
    }

    public (bool Success, T? Value) Pop()
    {
        if (_list.First == null)
        {
            if (ShowOutput) Print();
            return (false, default);
        }

        var result = _list.First.Value;
        _list.RemoveFirst();
        if (ShowOutput) Print();
        return (true, result);
    }

    public (bool Success, T? Value) Top()
    {
        if (_list.First == null)
            return (false, default);
        return (true, _list.First.Value);
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
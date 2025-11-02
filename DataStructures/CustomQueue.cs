namespace DataStructures;

public class CustomQueue<T>
{
    private readonly Queue<T?> _queue = new();

    public CustomQueue(bool showOutput)
    {
        ShowOutput = showOutput;
    }

    public bool ShowOutput { get; set; }
    public int Count => _queue.Count;

    public void Enqueue(T? item)
    {
        _queue.Enqueue(item);
        if (ShowOutput) Print();
    }

    public (bool Success, T? Value) Dequeue()
    {
        if (_queue.Count == 0)
        {
            if (ShowOutput) Print();
            return (false, default);
        }

        var result = _queue.Dequeue();
        if (ShowOutput) Print();
        return (true, result);
    }

    public (bool Success, T? Value) Peek()
    {
        if (_queue.Count == 0)
            return (false, default);
        return (true, _queue.Peek());
    }

    public bool IsEmpty => _queue.Count == 0;

    public void Print()
    {
        Console.WriteLine(this);
    }

    public override string ToString()
    {
        return "{ " + string.Join(", ", _queue) + " }";
    }
}
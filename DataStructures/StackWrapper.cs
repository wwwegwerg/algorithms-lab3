namespace DataStructures;

public class StackWrapper<T>
{
    private readonly Stack<T> _stack = new();

    public StackWrapper(bool showOutput)
    {
        ShowOutput = showOutput;
    }

    public bool ShowOutput { get; set; }
    public int Count => _stack.Count;

    public void Push(T item)
    {
        _stack.Push(item);
        if (ShowOutput) Print();
    }

    public T Pop()
    {
        if (_stack.Count == 0)
            throw new InvalidOperationException("The Stack is empty.");
        var result = _stack.Pop();
        if (ShowOutput) Print();
        return result;
    }

    public T Top()
    {
        if (_stack.Count == 0)
            throw new InvalidOperationException("The Stack is empty.");
        return _stack.Pop();
    }

    public bool IsEmpty => _stack.Count == 0;

    public void Print()
    {
        Console.WriteLine(this);
    }

    public override string ToString()
    {
        return "{ " + string.Join(", ", _stack) + " }";
    }
}
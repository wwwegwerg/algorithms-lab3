namespace DataStructures;

public class CustomStack<T> {
    private readonly DoublyLinkedList<T> _list = new();
    public bool ShowOutput { get; set; }
    public int Count => _list.Count;
    public bool IsEmpty => _list.Count == 0;

    public CustomStack(bool showOutput) {
        ShowOutput = showOutput;
    }

    public void Push(T item) {
        _list.AddFirst(item);
        if (ShowOutput) {
            Print();
        }
    }

    public T Pop() {
        if (_list.First == null) {
            throw new InvalidOperationException("The Stack is empty.");
        }

        var result = _list.RemoveFirst();
        if (ShowOutput) {
            Print();
        }

        return result;
    }

    public T Top() {
        if (_list.First == null) {
            throw new InvalidOperationException("The Stack is empty.");
        }

        return _list.First.Value;
    }

    public void Print() {
        Console.WriteLine(this);
    }

    public override string ToString() {
        return "{ " + string.Join(", ", _list) + " }";
    }
}
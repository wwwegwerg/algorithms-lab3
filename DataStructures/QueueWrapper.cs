namespace DataStructures;

public class QueueWrapper<T> {
    private readonly Queue<T> _queue = new();
    public bool ShowOutput { get; set; }
    public int Count => _queue.Count;
    public bool IsEmpty => _queue.Count == 0;

    public QueueWrapper(bool showOutput) {
        ShowOutput = showOutput;
    }

    public void Enqueue(T item) {
        _queue.Enqueue(item);
        if (ShowOutput) {
            Print();
        }
    }

    public T Dequeue() {
        if (_queue.Count == 0) {
            throw new InvalidOperationException("The Queue is empty.");
        }

        var result = _queue.Dequeue();
        if (ShowOutput) {
            Print();
        }

        return result;
    }

    public T Peek() {
        if (_queue.Count == 0) {
            throw new InvalidOperationException("The Queue is empty.");
        }

        return _queue.Peek();
    }

    public void Print() {
        Console.WriteLine(this);
    }

    public override string ToString() {
        return "{ " + string.Join(", ", _queue) + " }";
    }
}
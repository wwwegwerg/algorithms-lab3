namespace DataStructures;

public interface IDataStructure<T>
{
    int Count { get; }
    void Add(T? item);
    (bool Success, T? Value) Remove();
    (bool Success, T? Value) Peek();
    bool IsEmpty { get; }
    void Print();
}
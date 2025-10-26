namespace DataStructures;

public interface IDataStructure<T>
{
    int Count { get; }
    void Add(T? item);
    T? Remove();
    (bool success, T? value) Peek();
    bool IsEmpty { get; }
    void Print();
}
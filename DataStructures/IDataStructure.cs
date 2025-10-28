namespace DataStructures;

public interface IDataStructure<T>
{
    bool ShowOutput  { get; set; }
    int Count { get; }
    void Add(T? item);
    (bool Success, T? Value) Remove();
    (bool Success, T? Value) Peek();
    bool IsEmpty { get; }
    void Print();
}
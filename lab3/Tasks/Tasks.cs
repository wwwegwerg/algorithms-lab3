using DataStructures;
using lab3.Charts;

namespace lab3.Tasks;

public class Taskss
{
    public static void Stack()
    {
        var cd = Helpers.Build1DTime(
            "stack",
            "Количество операций",
            "Время (мс)",
            5,
            5,
            arr => () => Helpers.ParseData(arr, new CustomStack<string>())
        );
        
        ChartBuilder.Build2DLineChart(cd);
    }

    public static void PostfixEvaluation()
    {
    }

    public static void InfixToPostfix()
    {
    }

    public static void Queue()
    {
    }

    public static void Kadane()
    {
    }

    public static void ParenthesesEquations()
    {
    }

    public static void MovingAverage()
    {
    }

    public static void Huffman()
    {
    }

    public static void DoublyLinkedList()
    {
    }

    public static void BFS()
    {
    }

    public static void DFS()
    {
    }
}
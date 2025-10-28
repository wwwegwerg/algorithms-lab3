using DataStructures;
using lab3.Charts;

namespace lab3.Tasks;

public class Taskss
{
    public static void Stack()
    {
        var cd = Helpers.BenchStack(5, 5);
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
        var cd = Helpers.BenchQueue(5, 5);
        ChartBuilder.Build2DLineChart(cd);
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
using System.Text;
using DataStructures;

namespace lab3.Tasks;

public static class BFS
{
    public static void Run()
    {
        var root = new TreeNode<string>("A",
            new TreeNode<string>("B",
                new TreeNode<string>("D",
                    null,
                    new TreeNode<string>("G"))),
            new TreeNode<string>("C",
                new TreeNode<string>("E"),
                new TreeNode<string>("F",
                    new TreeNode<string>("H"),
                    new TreeNode<string>("J")))
        );

        var tree = new BinaryTree<string>(root);

        var result = TraverseTree(tree);
        Console.WriteLine("Результат: " + result);
    }

    public static string TraverseTree<T>(BinaryTree<T> tree)
    {
        if (tree.Root == null)
            return "*";

        var sb = new StringBuilder();
        var queue = new CustomListQueue<TreeNode<T>>(true);
        queue.Add(tree.Root);

        while (queue.Count > 0)
        {
            var current = queue.Remove().Value;
            if (current == null)
            {
                sb.Append('*');
                continue;
            }

            sb.Append(current.Value);
            queue.Add(current.Left);
            queue.Add(current.Right);
        }

        return sb.ToString();
    }
}
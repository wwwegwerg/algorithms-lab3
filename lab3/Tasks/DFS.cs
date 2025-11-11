using System.Text;
using DataStructures;

namespace lab3.Tasks;

public static class DFS {
    public static void Run() {
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

    private static string TraverseTree<T>(BinaryTree<T> tree) {
        if (tree.Root == null) {
            return "*";
        }

        var sb = new StringBuilder();
        var stack = new CustomStack<TreeNode<T>?>(true);
        stack.Push(tree.Root);

        while (!stack.IsEmpty) {
            var current = stack.Pop();
            if (current == null) {
                sb.Append('*');
                continue;
            }

            sb.Append(current.Value);
            stack.Push(current.Right);
            stack.Push(current.Left);
        }

        return sb.ToString();
    }
}
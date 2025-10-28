using System.Text;
using DataStructures;

namespace lab3.Tasks;

public static class DFS
{
    public static void Run()
    {
    }

    public static string TraverseTree<T>(BinaryTree<T> tree)
    {
        if (tree.Root == null)
            return "*";

        var stack = new CustomStack<TreeNode<T>>(true);
        stack.Add(tree.Root);
        var sb = new StringBuilder();

        while (stack.Count > 0)
        {
            var current = stack.Remove().Value;
            sb.Append(current.Value == null ? "*" : current.Value.ToString());

            if (current.Right != null)
                stack.Add(current.Right);
            if (current.Left != null)
                stack.Add(current.Left);
        }

        return sb.ToString();
    }
}
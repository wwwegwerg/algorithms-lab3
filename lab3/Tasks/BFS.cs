using System.Text;
using DataStructures;

namespace lab3.Tasks;

public static class BFS
{
    public static void Run()
    {
    }

    public static string TraverseTree<T>(BinaryTree<T> tree)
    {
        if (tree.Root == null)
            return "*";

        var queue = new CustomListQueue<TreeNode<T>>(true);
        queue.Add(tree.Root);
        var sb = new StringBuilder();

        while (queue.Count > 0)
        {
            var current = queue.Remove().Value;
            sb.Append(current.Value == null ? "*" : current.Value.ToString());

            if (current.Left != null)
                queue.Add(current.Left);
            if (current.Right != null)
                queue.Add(current.Right);
        }
        
        return sb.ToString();
    }
}
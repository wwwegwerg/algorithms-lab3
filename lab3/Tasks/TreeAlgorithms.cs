using DataStructures;

namespace lab3.Tasks;

public static class TreeAlgorithms
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

        var result = PathBetween(tree, "E", "J");
        Console.WriteLine("Результат: " + string.Join(" -> ", result));
    }

    private static List<T> PathBetween<T>(
        BinaryTree<T> tree, T a, T b, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;

        if (tree.Root is null) return [];

        var pathToA = new List<TreeNode<T>>();
        var pathToB = new List<TreeNode<T>>();

        if (!TryFindPath(tree.Root, a, pathToA, comparer)) return [];
        if (!TryFindPath(tree.Root, b, pathToB, comparer)) return [];

        var i = 0;
        while (i < pathToA.Count && i < pathToB.Count && ReferenceEquals(pathToA[i], pathToB[i]))
            i++;

        var lcaIndex = i - 1;

        var result = new List<T>();

        for (var j = pathToA.Count - 1; j > lcaIndex; j--)
            result.Add(pathToA[j].Value);

        result.Add(pathToA[lcaIndex].Value);

        for (var j = lcaIndex + 1; j < pathToB.Count; j++)
            result.Add(pathToB[j].Value);

        return result;
    }

    private static bool TryFindPath<T>(
        TreeNode<T>? root, T target,
        List<TreeNode<T>> path,
        IEqualityComparer<T> comparer)
    {
        if (root is null) return false;

        path.Add(root);

        if (comparer.Equals(root.Value, target))
            return true;

        if (TryFindPath(root.Left, target, path, comparer) ||
            TryFindPath(root.Right, target, path, comparer))
            return true;

        path.RemoveAt(path.Count - 1);
        return false;
    }
}
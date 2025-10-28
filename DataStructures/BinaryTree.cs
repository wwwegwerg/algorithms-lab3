namespace DataStructures;

public class TreeNode<T>
{
    public T Value { get; set; }
    public TreeNode<T>? Left { get; private set; }
    public TreeNode<T>? Right { get; private set; }
    public TreeNode<T>? Parent { get; private set; }
    public bool IsLeaf => Left is null && Right is null;

    public TreeNode(T value, TreeNode<T>? left = null, TreeNode<T>? right = null)
    {
        Value = value;
        SetLeft(left);
        SetRight(right);
    }

    public void SetLeft(TreeNode<T>? left)
    {
        Left = left;
        if (left != null) left.Parent = this;
    }

    public void SetRight(TreeNode<T>? right)
    {
        Right = right;
        if (right != null) right.Parent = this;
    }
}

public class BinaryTree<T>
{
    public TreeNode<T>? Root { get; private set; }

    public BinaryTree(TreeNode<T>? root) => Root = root;

    /// <summary>
    /// Обходит дерево и выдаёт пары (лист, путь), где путь — строка из '0' (влево) и '1' (вправо).
    /// Признак листа задаётся через предикат isLeafPredicate.
    /// </summary>
    public IEnumerable<(TreeNode<T> Node, string Path)> LeavesWithPaths(Func<TreeNode<T>, bool> isLeafPredicate)
    {
        if (Root is null) yield break;

        var stack = new Stack<(TreeNode<T> node, string path)>();
        stack.Push((Root, ""));

        while (stack.Count > 0)
        {
            var (node, path) = stack.Pop();

            if (isLeafPredicate(node))
            {
                yield return (node, path);
            }
            else
            {
                // Сначала вправо, затем влево — чтобы при использовании стека слева шли раньше (не критично)
                if (node.Right != null) stack.Push((node.Right, path + "1"));
                if (node.Left != null) stack.Push((node.Left, path + "0"));
            }
        }
    }
}
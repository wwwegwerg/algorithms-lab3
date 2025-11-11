namespace DataStructures;

public class TreeNode<T> {
    public T Value { get; set; }
    public TreeNode<T>? Left { get; private set; }
    public TreeNode<T>? Right { get; private set; }
    public TreeNode<T>? Parent { get; private set; }
    public bool IsLeaf => Left is null && Right is null;

    public TreeNode(T value, TreeNode<T>? left = null, TreeNode<T>? right = null) {
        Value = value;
        SetLeft(left);
        SetRight(right);
    }

    public void SetLeft(TreeNode<T>? left) {
        Left = left;
        if (left != null) {
            left.Parent = this;
        }
    }

    public void SetRight(TreeNode<T>? right) {
        Right = right;
        if (right != null) {
            right.Parent = this;
        }
    }

    public override string? ToString() {
        if (Value == null) {
            return "";
        }

        return Value.ToString();
    }
}

public class BinaryTree<T> {
    public TreeNode<T>? Root { get; private set; }
    public BinaryTree(TreeNode<T>? root) => Root = root;
}
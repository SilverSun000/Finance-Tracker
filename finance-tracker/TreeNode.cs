using System;

public class Tree<T> {
    public TreeNode<T> Root { get; private set; }
    public Tree() { Root = null; }
    public Tree (T rootData) { Root = new TreeNode<T>(rootData); }
    public TreeNode<T> AddNode(TreeNode<T> parent, T data) {
        var newNode = new TreeNode<T>(data);
        newNode.Parent = parent;
        parent.Children.Add(newNode);
        return newNode;
    }
    public void RemoveNode(TreeNode<T> node) {
        if (node.Parent != null) {
            node.Parent.Children.Remove(node);
        }
    }
    public void Traverse(TreeNode<T> currentNode, int depth) {
        if (currentNode != null) {
            Console.WriteLine($"{new string(' ', depth * 2)}{currentNode.Data}");

            foreach (var child in currentNode.Children) {
                Traverse(child, depth + 1);
            }
        }
    }
}

public class TreeNode<T> {
    public T Data { get; set; }
    public List<TreeNode<T>> Children { get; set; }
    public TreeNode<T> Parent { get; set; }

    public TreeNode(T data) { Data = data; Children = new List<TreeNode<T>>(); }
}

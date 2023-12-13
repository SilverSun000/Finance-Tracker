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

            if(currentNode.Children.Count == 0) {
                Console.WriteLine($"{new String(' ', (depth + 1) * 2)}Transactions:");
                foreach(var transaction in currentNode.Transactions) {
                    Console.WriteLine($"{new string(' ', (depth + 2) * 2)}Amount: {transaction.Amount}, Date: {transaction.Date}");
                }
            }

            foreach (var child in currentNode.Children) {
                Traverse(child, depth + 1);
            }
        }
    }
}

public class TreeNode<T> {
    public int Id { get; set; }
    public T Data { get; set; }
    public int? ParentId { get; set; }
    public List<TreeNode<T>> Children { get; set; }
    public TreeNode<T> Parent { get; set; }
    public List<Transaction> Transactions { get; set; }

    public TreeNode(T data) { 
        Data = data; 
        Children = new List<TreeNode<T>>();
        Transactions = new List<Transaction>();
    }
}

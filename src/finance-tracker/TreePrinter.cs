public static class TreePrinter
{
    public static void PrintTree(TreeNode<string> root)
    {
        PrintNode(root, 0);
    }

    private static void PrintNode(TreeNode<string> node, int depth)
    {
        if (node != null)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}{node.Data}");

            foreach (var transaction in node.Transactions)
            {
                Console.WriteLine($"{new string(' ', (depth + 1) * 2)}Amount: {transaction.Amount}, Date: {transaction.Date}");
            }

            foreach (var child in node.Children)
            {
                PrintNode(child, depth + 1);
            }
        }
    }
}
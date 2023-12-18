public class UserDashboard {
    private Tree<string> categoryTree;
    private readonly Credentials credentials;
    private readonly AppDbContext dbContext;
    private readonly User user;

    public UserDashboard(Credentials credentials, AppDbContext dbContext, User user) {
        this.credentials = credentials;
        this.dbContext = dbContext;
        this.user = user;
    }

    public void DislayMenu() {
        while (true) {
            Console.Clear();
            Console.WriteLine(
                """
                1. Add Category
                2. Remove Category
                3. Display Tree
                4. Exit
                """
            );

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice) {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    RemoveCategory();
                    break;
                case "3":
                categoryTree = FetchTree(user);
                    DisplayTree();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void AddCategory() {
        categoryTree = FetchTree(user);

        if (categoryTree != null && categoryTree.Root != null) {
            Console.Write("Enter category name: ");
            string categoryName = Console.ReadLine();

            var newNode = categoryTree.AddNode(categoryTree.Root, categoryName);

            Console.WriteLine($"Category '{categoryName}' added with ID {newNode.Id}.");
        } else {
            Console.WriteLine("Error: Category tree or root is null.");
        }
        Console.ReadKey();
    }
    private void RemoveCategory() {
        categoryTree = FetchTree(user);
        Console.Write("Enter category name to remove: ");
        string categoryName = Console.ReadLine();

        var nodeToRemove = FindNodeByName(categoryTree.Root, categoryName);

        if (nodeToRemove != null) {
            categoryTree.RemoveNode(nodeToRemove);
            Console.WriteLine($"Category '{categoryName}' removed.");
        } else {
            Console.WriteLine($"Category '{categoryName}' not found.");
        }

        Console.ReadKey();
    }
    private void DisplayTree() {
        Console.WriteLine("Category Tree: ");
        categoryTree.Traverse(categoryTree.Root, 0);
        Console.ReadKey();
    }
    private TreeNode<string> FindNodeByName(TreeNode<string> currentNode, string categoryName) {
        if (currentNode != null) {
            if (currentNode.Data == categoryName) {
                return currentNode;
            }
            foreach (var child in currentNode.Children) {
                var result = FindNodeByName(child, categoryName);
                if (result != null) {
                    return result;
                }
            }
        }
        return null;
    }
    public Tree<string> FetchTree(User user) {
        Console.WriteLine($"Fetching tree for user {user.Id}...");
        var categoryTree = dbContext.GetTree(user.Id);
        Console.WriteLine(user.CategoryTreeId);
    
        if (categoryTree == null) {
            Console.WriteLine("Tree not found, creating a new one...");
            categoryTree = new Tree<string>();
            user.CategoryTree = categoryTree;
            dbContext.SaveChanges();
        } else {
            Console.WriteLine("Tree found.");
        }

        return categoryTree;
    }
}
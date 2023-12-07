using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string credPath = "../userCredentials.txt";

        var credentials = new Credentials();
        credentials.TryLoadFrom(credPath);
        

        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                    """
                    1. Register
                    2. Login
                    3. Clear all credentials
                    4. Exit
                    """);

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    Register(credentials);
                    credentials.Save(credPath);
                    Console.ReadKey();
                    break;
                case "2":
                    TryLogin(credentials);
                    Console.ReadKey();
                    break;
                case "3":
                    credentials.Clear();
                    credentials.Save(credPath);

                    Console.Write("All cleared.");
                    break;
                case "4":
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static (string name, string userID, string pass) RequestCredentials()
    {
        Console.Write("Enter your UserID: ");
        var newUserID = Console.ReadLine();

        Console.Write("Enter username: ");
        var newUsername = Console.ReadLine();

        Console.Write("Enter password: ");
        var newPassword = Console.ReadLine();

        return (newUserID, newUsername, newPassword);
    }

    static User Register(Credentials creds)
    {
        var login = RequestCredentials();
        var user = new User(login.name, login.userID);

        creds.Register(login.name, login.pass);

        Console.WriteLine("Registration successful!");
        return user;
    }


    static User TryLogin(Credentials creds)
    {
        var login = RequestCredentials();

        if (creds.TryAuthenticate(login.name, login.pass))
        {
            Console.WriteLine("Login successful!");

            return new User(login.name, login.userID);
        }

        Console.WriteLine("Invalid username or password.");

        return null;
    }

    static User checkTree(Credentials creds) {
        //Assign UserID to an empty tree if they don't have one
        //Create a boolean property for User, registered accounts auto given tree
        //User input for categories
        var login = RequestCredentials();
        Tree<string> thingy = Boomer();
        

        if (creds.TryAuthenticate(login.name, login.pass)) {
            Tree<string> categoryTree = new Tree<string>("Root");
            treeHolder.Add(login.userID, categoryTree);
        }
    }
    static Tree<string> Boomer() {
        Tree<string> categoryTree = new Tree<string>("Root");
        return categoryTree;
    }
}

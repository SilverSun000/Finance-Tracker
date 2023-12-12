using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;


class Program {
    static void Main() {
        string credPath = "../userCredentials.txt";

        using (var dbContext = new AppDbContext()) {
            dbContext.Database.EnsureCreated();

            var credentials = new Credentials(dbContext);

            while (true)
            {
                //Console.Clear();
                Console.WriteLine(
                    """
                    1. Register
                    2. Login
                    3. Exit
                    """
                );

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        Register(credentials, dbContext);
                        Console.ReadKey();

                        break;
                    case "2":
                        TryLogin(credentials, dbContext);
                        Console.ReadKey();

                        break;
                    case "3":
                        Console.Clear();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
    static (string name, string pass) RequestCredentials() {
        Console.Write("Enter Username: ");
        var newUsername = Console.ReadLine();

        Console.Write("Enter Password: ");
        var newPassword = Console.ReadLine();

        return (newUsername, newPassword);
    }

    static void Register(Credentials creds, AppDbContext dbContext) {
        var login = RequestCredentials();

        var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == login.name);

        if (existingUser != null)
        {
            Console.WriteLine("User already exists. Registration failed.");
            Console.ReadKey();
        }
        else
        {
            // Create a new User entity and add it to the database
            var newUser = new User { Username = login.name };
            string hashedPassword = Credentials.Hash(login.pass);
            newUser.Password = hashedPassword;

            dbContext.Users.Add(newUser);

            // Save changes to the database
            dbContext.SaveChanges();

            PrintDatabaseContents(dbContext);

            // Register the user in the Credentials service
            creds.Register(login.name, login.pass);

            Console.WriteLine("Registration Complete.");
            Console.ReadKey();
        }
    }


    static User TryLogin(Credentials creds, AppDbContext dbContext) {
        var login = RequestCredentials();

        // Fetch the user from the database
        var user = dbContext.Users.FirstOrDefault(u => u.Username == login.name);

        if (user != null)
        {
            // Check if the entered password matches the stored hashed password
            if (creds.TryAuthenticate(login.name, login.pass))
            {
                Console.WriteLine("Login Successful");
                return new User(login.name);
            }
        }

        Console.WriteLine("Invalid username or password.");
        return null;
    }

    static void PrintDatabaseContents(AppDbContext dbContext)
    {
        // Print the contents of the Users table to the console
        Console.WriteLine("Users in the Database:");
        foreach (var user in dbContext.Users)
        {
            Console.WriteLine($"Username: {user.Username}, Password: {user.Password}");
        }
        Console.WriteLine();
    }
}
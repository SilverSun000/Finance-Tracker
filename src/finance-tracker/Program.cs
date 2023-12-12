using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// TODO:
/// -Put all static service functions in their own class (So program.cs is cleaner)
/// -Connect Tree (for income / spending categories) to each individual user and save it to the database.
/// -Allow User to remove and add categories / transactions once they've logged in.
/// 
/// 
/// -for Scabbage: Write Comments, no change code plez I beg.
/// </summary>
class Program {
    static void Main() {
        using (var dbContext = new AppDbContext()) {
            dbContext.Database.EnsureCreated();

            var credentials = new Credentials(dbContext);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    """
                    1. Register
                    2. Login
                    3. Clear Database
                    4. Print Database
                    5. Exit
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
                        ClearDatabase(dbContext);
                        Console.Write("Database has been wiped.");
                        Console.ReadKey();

                        break;
                    case "4":
                        PrintDatabaseContents(dbContext);
                        Console.ReadKey();

                        break;
                    case "5":
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
            var newUser = new User { Username = login.name };
            string hashedPassword = Credentials.Hash(login.pass);
            newUser.Password = hashedPassword;

            dbContext.Users.Add(newUser);

            dbContext.SaveChanges();

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
        Console.WriteLine("Users in the Database:");
        foreach (var user in dbContext.Users)
        {
            Console.WriteLine($"Username: {user.Username}, Password: {user.Password}");
        }
        Console.WriteLine();
    }
    public static void ClearDatabase(AppDbContext dbContext) {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
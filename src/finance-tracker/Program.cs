using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Newtonsoft.Json;

class Program {
    static void Main() {
        string credPath = "../userCredentials.txt";

        using (var dbContext = new AppDbContext()) {
            dbContext.Database.EnsureCreated();

            var credentials = new Credentials();
            credentials.TryLoadFrom(credPath);

            while (true)
            {
                //Console.Clear();
                Console.WriteLine(
                    """
                    1. Register
                    2. Login
                    3. Clear all credentials
                    4. Exit
                    """
                );

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        Register(credentials, dbContext);
                        credentials.Save(credPath);

                        break;
                    case "2":
                        TryLogin(credentials);
                        Console.ReadKey();

                        break;
                    case "3":
                        credentials.Clear();
                        credentials.Save(credPath);

                        Console.WriteLine("All Cealred.");
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

        try
        {
            // Check if the user already exists in the database
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
                dbContext.Users.Add(newUser);

                // Save changes to the database
                dbContext.SaveChanges();

                // Register the user in the Credentials service
                creds.Register(login.name, login.pass);

                Console.WriteLine("Registration Complete.");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.ReadKey();
            // You might want to log the exception for more detailed analysis
        }
        Console.ReadKey();
    }


    static User TryLogin(Credentials creds) {
        var login = RequestCredentials();

        if (creds.TryAuthenticate(login.name, login.pass)) {
            Console.WriteLine("Login Successful");

            return new User(login.name);
        }

        Console.WriteLine("Invalid username or password.");

        return null;
    }
}
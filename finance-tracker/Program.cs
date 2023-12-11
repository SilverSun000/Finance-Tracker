using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program {
    static void Main() {
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
                """
            );

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    Register(credentials);
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

    static (string name, string pass) RequestCredentials() {
        Console.Write("Enter Username: ");
        var newUsername = Console.ReadLine();

        Console.Write("Enter Password: ");
        var newPassword = Console.ReadLine();

        return (newUsername, newPassword);
    }

    static User Register(Credentials creds) {
        var login = RequestCredentials();
        var user = new User(login.name);

        creds.Register(login.name, login.pass);

        Console.WriteLine("Registration Complete.");
        return user;
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
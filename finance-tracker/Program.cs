using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program {
    static void Main(string[] args) {
        User test = new User();

        string filePath = "C:/Users/Billson/Documents/Github/Finance-Tracker/userCredentials.txt";
        var credentials = test.loadCredentials(filePath);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter username: ");
                    string registerUsername = Console.ReadLine();
                    Console.Write("Enter password: ");
                    string registerPassword = Console.ReadLine();
                    test.RegisterUser(credentials, registerUsername, registerPassword);
                    test.SaveCredentials(filePath, credentials);
                    Console.WriteLine("Registration successful!");
                    break;

                case "2":
                    Console.Write("Enter username: ");
                    string loginUsername = Console.ReadLine();
                    Console.Write("Enter password: ");
                    string loginPassword = Console.ReadLine();
                    if (test.AuthenticateUser(credentials, loginUsername, loginPassword))
                        Console.WriteLine("Login successful!");
                    else
                        Console.WriteLine("Invalid username or password.");
                    break;

                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
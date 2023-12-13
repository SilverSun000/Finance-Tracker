﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// - Create a Dashboard once the user has logged in
/// - User can view total transactions associated with their account.
/// - User can view which categories are associated with each transactions (rent, grocery, etc)
/// - User can Remove & Edit categories / transactions if necessary
/// - User can Add categories / transactios if necessary
/// 
/// - For Scabbage: Write Comments, no change code plez I beg.
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
                        UserService.Register(credentials, dbContext);
                        Console.ReadKey();

                        break;
                    case "2":
                        UserService.TryLogin(credentials, dbContext);
                        Console.ReadKey();

                        break;
                    case "3":
                        UserService.ClearDatabase(dbContext);
                        Console.Write("Database has been wiped.");
                        Console.ReadKey();

                        break;
                    case "4":
                        UserService.PrintUsers(dbContext);
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
}
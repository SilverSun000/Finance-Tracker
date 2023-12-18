using System;
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
class Program
{
    static void Main()
    {
        using (var dbContext = new AppDbContext())
        {
            dbContext.Database.EnsureCreated();

            var credentials = new Credentials(dbContext);
            User loggedInUser = null;
            bool isLoggedIn = false; // Declare outside the while loop
            UserDashboard userDashboard = null;

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
                        Credentials.Register(credentials, dbContext);
                        Console.ReadKey();
                        break;
                    case "2":
                        loggedInUser = Credentials.TryLogin(credentials, dbContext);
                        isLoggedIn = loggedInUser != null;
                        if (isLoggedIn) {
                            var categoryTree = userDashboard.FetchTree(loggedInUser);
                            userDashboard = new UserDashboard(credentials, dbContext, loggedInUser);
                            userDashboard.DislayMenu();
                        }
                        Console.ReadKey();
                        break;
                    case "3":
                        Credentials.ClearDatabase(dbContext);
                        Console.Write("Database has been wiped.");
                        isLoggedIn = false;
                        Console.ReadKey();
                        break;
                    case "4":
                        Credentials.PrintUsers(dbContext);
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
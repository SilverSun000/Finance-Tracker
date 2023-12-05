using System;
using System.IO;

public class User : IUserSetup {
    private userInfo testStruct;
    private Dictionary<string, string> hash = new Dictionary<string, string>();
    public string UserID {
        get { return testStruct.userID; }
        set { testStruct.userID = value; }
    }
    public string Username {
        get { return testStruct.username; }
        set { testStruct.username = value; }
    }
    public string Password {
        get { return testStruct.password; }
        set { testStruct.password = value; }
    }
    public void createNewUser() {
        Console.WriteLine("Enter user ID:");
        testStruct.userID = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter username:");
        testStruct.username = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter password:");
        testStruct.password = Console.ReadLine() ?? string.Empty;

        hash.Add(Username, Password);
        //Console.WriteLine(Username + " " + Password);
    }

    public void Login(string username, string password) {
        if (hash.TryGetValue(username, out var value)) {
            Console.WriteLine("Access Granted!");
        }
        else {
            Console.WriteLine("Account doesn't exist!");
        }
    }
    public void showUserInfo() {
        Console.WriteLine(testStruct.userID);
        Console.WriteLine(testStruct.username);
        Console.WriteLine(testStruct.password);
    }
}
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class Credentials
{
    private readonly AppDbContext dbContext;
    public Credentials(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public static (string name, string pass) RequestCredentials() {
        Console.Write("Enter Username: ");
        var newUsername = Console.ReadLine();

        Console.Write("Enter Password: ");
        var newPassword = Console.ReadLine();

        return (newUsername, newPassword);
    }
    public static void Register(Credentials creds, AppDbContext dbContext) {
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

            Console.WriteLine("Registration Complete.");
            Console.ReadKey();
        }
    }
    public static User TryLogin(Credentials creds, AppDbContext dbContext) {
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
    public static void PrintUsers(AppDbContext dbContext)
    {
        Console.WriteLine("Users in the Database:");
        foreach (var user in dbContext.Users.AsNoTracking().ToList())
        {
            Console.WriteLine($"Username: {user.Username}, Password: {user.Password}");
        }
        Console.WriteLine();
    }
    public static void ClearDatabase(AppDbContext dbContext) {
        foreach(var entity in dbContext.ChangeTracker.Entries().ToList()) {
            entity.State = EntityState.Detached;
        }
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        
    }
  
  
  
    public bool TryGet(string username, out string passwordHash)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Username == username);

        if (user != null)
        {
            passwordHash = user.Password;
            return true;
        }

        passwordHash = null;
        return false;
    }
    public bool TryAuthenticate(string username, string loginPassword)
    {
        if (TryGet(username, out var storedHash))
        {
            var hashedPassword = Hash(loginPassword);
            return hashedPassword == storedHash;
        }

        return false;
    }
    public static string Hash(string password)
    {
        using (var algo = SHA256.Create())
        {
            var hashBytes = algo.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }
}

public class UserService {
    public readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext) {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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

            creds.Register(login.name, login.pass);

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
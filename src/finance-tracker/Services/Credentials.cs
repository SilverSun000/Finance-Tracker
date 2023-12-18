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

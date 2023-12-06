using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class Credentials
{
    Dictionary<string, string> userCreds = new();

    public bool TryLoadFrom(string path)
    {
        Dictionary<string, string>? newCreds;

        try
        {
            string json = File.ReadAllText(path);
            newCreds = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
        catch (FileNotFoundException)
        {
            return false;
        }

        foreach (var kv in newCreds)
        {
            var name = kv.Key;
            var hash = kv.Value;

            userCreds.Remove(name);
            userCreds.Add(name, hash);
        }

        return true;
    }


    public void Save(string filePath)
    {
        string json = JsonConvert.SerializeObject(userCreds, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public void Register(string username, string password)
    {
        var passHash = Hash(password);

        userCreds.Remove(username);
        userCreds.Add(username, passHash);
    }

    static string Hash(string password)
    {
        using (var algo = SHA256.Create())
        {
            var hashBytes = algo.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashStr = Convert.ToBase64String(hashBytes);
            return hashStr;
        }
    }

    public bool TryGet(string username, out string passwordHash)
    {
        return userCreds.TryGetValue(username, out passwordHash);
    }

    public bool TryAuthenticate(string username, string loginPassword)
    {
        if(!TryGet(username, out var storedHash))
            return false;

        var hp = Hash(loginPassword);
        return hp == storedHash;
    }

    public void Clear()
    {
        userCreds.Clear();
    }
}


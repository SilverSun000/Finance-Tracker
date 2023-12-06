using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class User : IUserSetup {
    public Dictionary<string, string> loadCredentials(string filePath) {
        try {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
        catch (FileNotFoundException) {
            return new Dictionary<string, string>();
        }
    }
    public void SaveCredentials(string filePath, Dictionary<string, string> credentials) {
        string json = JsonConvert.SerializeObject(credentials, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
    public void RegisterUser(Dictionary<string, string> credentials, string username, string password) {
        credentials[username] = password;
    }
    public bool AuthenticateUser(Dictionary<string, string> credentials, string username, string password) {
        return credentials.TryGetValue(username, out string storedPassword) && storedPassword == password;
    }
    public void DeleteCredentials(string filePath, Dictionary<string, string> credentials) {
        credentials.Clear();
        string json = JsonConvert.SerializeObject(new Dictionary<string, string>(), Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
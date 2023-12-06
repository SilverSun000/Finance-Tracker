public interface IUserSetup { //Don't really need this tbh
    public Dictionary<string, string> loadCredentials(string filePath);
    public void SaveCredentials(string filePath, Dictionary<string, string> credentials);
    public void RegisterUser(Dictionary<string, string> credentials, string username, string password);
    public bool AuthenticateUser(Dictionary<string, string> credentials, string username, string password);
}
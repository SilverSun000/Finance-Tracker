using Newtonsoft.Json;

public class User
{
    string Username { get; }
    string UserID { get; }

    public User(string username, string userID) { Username = username;  UserID = userID; }
}

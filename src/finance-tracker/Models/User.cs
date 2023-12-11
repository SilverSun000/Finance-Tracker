using Newtonsoft.Json;

public class User
{
    public int Id { get; set; }
    string Username { get; set; }
    public User() {}
    public User(string username) : this() { Username = username; }
}

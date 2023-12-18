using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? CategoryTreeId { get; set; }
    public Tree<string> CategoryTree { get; set; }
    public User() { CategoryTree = new Tree<string>("Root"); CategoryTreeId = CategoryTree.Root.Id; }
    public User(string username) : this() { Username = username; }
}



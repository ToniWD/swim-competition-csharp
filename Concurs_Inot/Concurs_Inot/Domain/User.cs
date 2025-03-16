namespace Concurs_Inot.Domain;

public class User
{
    public String username { get; set; }
    public String password { get; set; }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
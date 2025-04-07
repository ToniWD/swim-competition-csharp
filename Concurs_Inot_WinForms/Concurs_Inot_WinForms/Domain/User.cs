namespace Concurs_Inot_WinForms.Domain;

public class User : Entity<long>
{
    public string username { get; set; }
    public string password { get; set; }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
namespace Concurs_Inot_WinForms.Domain;

public class Participant : Entity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public int nrEvents { get; set; }

    public Participant(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}
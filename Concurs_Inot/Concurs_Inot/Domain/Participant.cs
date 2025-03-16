namespace Concurs_Inot.Domain;

public class Participant : Entity<long>
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public int Age { get; set; }

    public Participant(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}
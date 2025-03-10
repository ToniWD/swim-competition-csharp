namespace Concurs_Iot.Domain;

public class Participant : Entity<long>
{
    private String FirstName { get; set; }
    private String LastName { get; set; }
    private int Age { get; set; }

    public Participant(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}
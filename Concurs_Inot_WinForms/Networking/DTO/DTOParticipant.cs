using Model.Domain;

namespace Networking.DTO
{
    public class DTOParticipant : Entity<long>
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public int nrEvents { get; set;}

        public DTOParticipant(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            nrEvents = -1;
        }

    }
}

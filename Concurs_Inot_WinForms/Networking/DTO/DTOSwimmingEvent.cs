using Model.Domain;

namespace Networking.DTO
{
    public class DTOSwimmingEvent : Entity<long>
    {
        public int distance { get; set; }
        public string style { get; set; }
        public int nrParticipants { get; set; } = -1;

        public DTOSwimmingEvent(int distance, string style)
        {
            this.distance = distance;
            this.style = style;
        }

    }
}

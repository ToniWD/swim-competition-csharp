using Model.Domain;
using Networking.DTO;
using System;

namespace Networking.JsonProtocol
{
    public class Request
    {
        public RequestType type { get; set; }
        public User user { get; set; }
        public DTOSwimmingEvent[] swimmingEvents { get; set; }
        public String nameFilter { get; set; }
        public DTOSwimmingEvent swimmingEvent { get; set; }
        public DTOParticipant participant { get; set; }

        public Request() { }
    }
}

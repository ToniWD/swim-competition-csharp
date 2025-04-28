using Networking.DTO;
using System;

namespace Networking.JsonProtocol
{
    [Serializable]
    public class Response
    {
        public ResponseType Type { get; set; }
        public string ErrorMessage { get; set; }
        public DTOSwimmingEvent[] SwimmingEvents { get; set; }
        public DTOParticipant[] Participants { get; set; }

        public Response()
        {
        }

    }
}

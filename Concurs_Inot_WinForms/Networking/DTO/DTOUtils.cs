using Model.Domain;

namespace Networking.DTO
{
    public class DTOUtils
    {
        public static SwimmingEvent getFromDTO(DTOSwimmingEvent dto)
        {
            SwimmingEvent obj = new SwimmingEvent(dto.distance, dto.style);
            obj.nrParticipants = dto.nrParticipants;
            obj.Id = dto.Id;
            return obj;
        }

        public static DTOSwimmingEvent getDTO(SwimmingEvent obj)
        {
            DTOSwimmingEvent dto = new DTOSwimmingEvent(obj.Distance, obj.Style);
            dto.nrParticipants = obj.nrParticipants;
            dto.Id = obj.Id;
            return dto;
        }

        public static SwimmingEvent[] getFromDTO(DTOSwimmingEvent[] dto)
        {
            SwimmingEvent[] objs = new SwimmingEvent[dto.Length];
            for (int i = 0; i < dto.Length; i++)
            {
                objs[i] = getFromDTO(dto[i]);
            }
            return objs;
        }

        public static DTOSwimmingEvent[] getDTO(SwimmingEvent[] objs)
        {
            DTOSwimmingEvent[] dto = new DTOSwimmingEvent[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                dto[i] = getDTO(objs[i]);
            }
            return dto;
        }




        public static Participant getFromDTO(DTOParticipant dto)
        {
            Participant obj = new Participant(dto.firstName, dto.lastName, dto.age);
            obj.Id = dto.Id;
            obj.nrEvents = dto.nrEvents;
            return obj;
        }

        public static DTOParticipant getDTO(Participant obj)
        {
            DTOParticipant dto = new DTOParticipant(obj.FirstName, obj.LastName, obj.Age);
            dto.nrEvents = obj.nrEvents;
            dto.Id = obj.Id;
            return dto;
        }

        public static Participant[] getFromDTO(DTOParticipant[] dto)
        {
            Participant[] objs = new Participant[dto.Length];
            for (int i = 0; i < dto.Length; i++)
            {
                objs[i] = getFromDTO(dto[i]);
            }
            return objs;
        }

        public static DTOParticipant[] getDTO(Participant[] objs)
        {
            DTOParticipant[] dto = new DTOParticipant[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                dto[i] = getDTO(objs[i]);
            }
            return dto;
        }

    }
}

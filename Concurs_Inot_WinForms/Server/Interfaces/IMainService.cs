using Model.Domain;
using Service.Interfaces;

namespace Server.Interfaces
{
    public interface IMainService
    {
        IEnumerable<SwimmingEvent> GetSwimmingEvents();

        IEnumerable<Participant> GetParticipantsForEvent(long eventId);

        IEnumerable<Participant> GetParticipantsForEvent(long eventId, string fullname);

        void addParticipant(string firstName, string lastName, int age, IEnumerable<long> swimmingEvents);
    
        void addClient(User user, IObserver client);

        IObserver removeClient(User user);
    }
}

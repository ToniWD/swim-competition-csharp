using Model.Domain;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IServices
    {
        IEnumerable<SwimmingEvent> GetSwimmingEvents();

        IEnumerable<Participant> GetParticipantsForEvent(SwimmingEvent ev);

        IEnumerable<Participant> GetParticipantsForEvent(SwimmingEvent ev, string fullname);

        void addParticipant(string firstName, string lastName, int age, IEnumerable<SwimmingEvent> swimmingEvents);

        bool login(string username, string password, IObserver client);

        void logout(User user, IObserver client);

        void setClient(IObserver client);
    }
}

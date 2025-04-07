using Concurs_Inot_WinForms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs_Inot_WinForms.Service.Interfaces
{
    public interface IMainService
    {
        IEnumerable<SwimmingEvent> GetSwimmingEvents();

        IEnumerable<Participant> GetParticipantsForEvent(long eventId);

        IEnumerable<Participant> GetParticipantsForEvent(long eventId, string fullname);

        void addParticipant(String firstName, String lastName, int age, IEnumerable<long> swimmingEvents);
    }
}

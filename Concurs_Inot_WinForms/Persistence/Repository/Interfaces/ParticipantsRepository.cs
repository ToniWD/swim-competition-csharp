using Model.Domain;
using System.Collections.Generic;

namespace Persistence.Repository.Interfaces
{
    public interface ParticipantsRepository : IRepository<long, Participant>
    {
        IEnumerable<Participant> GetParticipantsForEvent(long eventId);

        IEnumerable<Participant> GetParticipantsForEvent(long eventId, string fullname);
    }
}
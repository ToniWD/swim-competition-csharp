using Concurs_Inot_WinForms.Domain;
using System.Collections.Generic;

namespace Concurs_Inot_WinForms.Repository.Interfaces;

public interface ParticipantsRepository : IRepository<long, Participant>
{
    IEnumerable<Participant> GetParticipantsForEvent(long eventId);

    IEnumerable<Participant> GetParticipantsForEvent(long eventId, string fullname);
}
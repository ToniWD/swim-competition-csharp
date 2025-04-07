using Concurs_Inot_WinForms.Domain;
using System.Collections;
using System.Collections.Generic;

namespace Concurs_Inot_WinForms.Repository.Interfaces;

public interface RecordsRepository : IRepository<long, Record>
{
    void SaveRecordsForParticipant(long participantId, IEnumerable<long> events);
}
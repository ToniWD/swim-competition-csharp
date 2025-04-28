using Model.Domain;
using System.Collections;
using System.Collections.Generic;

namespace Persistence.Repository.Interfaces
{
    public interface RecordsRepository : IRepository<long, Record>
    {
        void SaveRecordsForParticipant(long participantId, IEnumerable<long> events);
    }
}
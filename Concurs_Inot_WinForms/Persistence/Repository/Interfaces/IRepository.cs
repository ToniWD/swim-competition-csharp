using Model.Domain;
using System.Collections.Generic;

namespace Persistence.Repository.Interfaces
{
    public interface IRepository<TId, TE> where TE : Entity<TId>
    {
        TE FindOne(TId id);

        IEnumerable<TE> FindAll();

        TE Save(TE entity);

        bool Delete(TId id);
    }
}
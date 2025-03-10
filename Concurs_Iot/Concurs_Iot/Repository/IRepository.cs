using Concurs_Iot.Domain;

namespace Concurs_Iot.Repository;

public interface IRepository<TId, TE> where TE : Entity<TId>
{
    TE FindOne(TId id);

    IEnumerable<TE> FindAll();

    TE Save(TE entity);

    bool Delete(TId id);
}
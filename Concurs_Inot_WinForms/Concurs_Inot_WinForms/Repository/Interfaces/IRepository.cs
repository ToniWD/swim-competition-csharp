using Concurs_Inot_WinForms.Domain;
using System.Collections.Generic;

namespace Concurs_Inot_WinForms.Repository.Interfaces;

public interface IRepository<TId, TE> where TE : Entity<TId>
{
    TE FindOne(TId id);

    IEnumerable<TE> FindAll();

    TE Save(TE entity);

    bool Delete(TId id);
}
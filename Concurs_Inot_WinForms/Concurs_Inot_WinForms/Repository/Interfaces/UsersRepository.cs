using Concurs_Inot_WinForms.Domain;

namespace Concurs_Inot_WinForms.Repository.Interfaces;

public interface UsersRepository : IRepository<long, User>
{
    User findByUsername(string username);
}
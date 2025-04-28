using Model.Domain;

namespace Persistence.Repository.Interfaces
{
    public interface UsersRepository : IRepository<long, User>
    {
        User findByUsername(string username);
    }
}
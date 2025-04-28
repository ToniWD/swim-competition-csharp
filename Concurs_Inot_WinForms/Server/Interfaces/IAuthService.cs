

namespace Server.Interfaces
{
    public interface IAuthService
    {
        bool authentificate(string username, string password);
    }
}

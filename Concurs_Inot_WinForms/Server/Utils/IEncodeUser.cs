using Model.Domain;

namespace Server.Utils
{
    public interface IEncodeUser
    {
        void encryptPassword(User user);

        bool verifyPassword(User user, string password);
    }
}

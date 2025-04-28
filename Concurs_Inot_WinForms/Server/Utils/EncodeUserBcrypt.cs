using Model.Domain;

namespace Server.Utils
{
    public class EncodeUserBcrypt : IEncodeUser
    {
        public void encryptPassword(User user)
        {
            string pwd = user.username + user.password;
            user.password = BCrypt.Net.BCrypt.HashPassword(pwd);
        }

        public bool verifyPassword(User user, string password)
        {

            string pwd = user.username + password;

            //Console.WriteLine(pwd);
            //Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(pwd));
            return BCrypt.Net.BCrypt.Verify(pwd, user.password);
        }
    }
}

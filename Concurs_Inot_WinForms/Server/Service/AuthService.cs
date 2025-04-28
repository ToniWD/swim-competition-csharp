using Model.Domain;
using Persistence.Repository.Interfaces;
using Server.Interfaces;
using Server.Utils;
using Service.Utils;
using log4net;

namespace Server.Service
{
    public class AuthService : IAuthService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AuthService));
        private UsersRepository usersRepo;
        private IEncodeUser encodeUser = new EncodeUserBcrypt();

        public AuthService(UsersRepository usersRepo)
        {
            this.usersRepo = usersRepo;
        }


        public bool authentificate(string username, string password)
        {
            logger.Info("Authenticating user " + username);

            if (username == null || username.Length == 0)
            {
                throw new ServiceException("Username cannot be null or empty");
            }

            if (password == null || password.Length == 0)
            {
                throw new ServiceException("Password cannot be null or empty");
            }

            User user = usersRepo.findByUsername(username);
            //Console.WriteLine(user.password);

            if (user != null)
            {
                return encodeUser.verifyPassword(user, password);
            }
            else
            {
                logger.Warn("User " + username + " not found");
                throw new ServiceException("User not found");
            }
        }
    }
}

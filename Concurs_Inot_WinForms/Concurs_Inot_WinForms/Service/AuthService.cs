using Concurs_Inot_WinForms.Domain;
using Concurs_Inot_WinForms.Repository.DBRepositories;
using Concurs_Inot_WinForms.Repository.Interfaces;
using Concurs_Inot_WinForms.Service.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs_Inot_WinForms.Service
{
    public class AuthService : IAuthService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AuthService));
        private UsersRepository usersRepo;

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

            if (user != null)
            {
                return user.password == password;
            }
            else
            {
                logger.Warn("User " + username + " not found");
                throw new ServiceException("User not found");
            }
        }
    }
}

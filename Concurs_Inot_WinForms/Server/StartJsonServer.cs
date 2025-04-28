using Model.Domain.Validator;
using Networking.Utils;
using Persistence.Repository.DBRepositories;
using Server.Interfaces;
using Server.Service;
using Service.Interfaces;
using log4net;
using log4net.Config;
using System.Configuration;

namespace Server
{
    public class StartJsonServer
    {
        private static int defaultPort = 8080;
        private static ILog log = LogManager.GetLogger(typeof(StartJsonServer));

        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            log.Info("Starting application...");

            String connectionString = ConfigurationManager.ConnectionStrings["SwimmingCompetitionDB"].ConnectionString;


            IMainService mainService = new UserService(
            new ParticipantsDbRepo(connectionString, new ParticipantValidator()),
            new SwimmingEventsDbRepo(connectionString),
            new RecordsDBRepo(connectionString)
        );
            IAuthService authService = new AuthService(new UsersDbRepo(connectionString));
            //authService.authentificate("user1", "password123");

            IServices services = new MainServices(authService, mainService);

            int serverPort = defaultPort;

            string portString = ConfigurationManager.AppSettings["server.port"];

            if (int.TryParse(portString, out int port))
            {
                log.Info($"Portul serverului este: {port}");
                serverPort = port;
            }
            else
            {
                log.Error("Portul nu este valid!");
            }

            log.Debug($"Serverul va rula pe portul: {serverPort}");
            AbstractServer server = new JsonConcurrentServer(serverPort, services);

            try
            {
                server.Start();
            }
            catch (Exception e)
            {
                log.Error("Error starting server: " + e);
            }

            log.Info("Closing application...");
        }
    }
}

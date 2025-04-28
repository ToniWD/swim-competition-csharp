using Networking.JsonProtocol;
using Service.Interfaces;
using UI;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public static class StartClient
    {
        private static int defaultPort = 55555;
        private static string defaultServer = "localhost";
        private static readonly ILog log = LogManager.GetLogger(typeof(StartClient));
        
        [STAThread]
        static void Main()
        {
            // Configurează log4net folosind fișierul XML
            XmlConfigurator.Configure(new FileInfo("log4netClient.config"));

            log.Info("Starting application...");


            string serverIP = ConfigurationManager.AppSettings["server.host"];
            int serverPort = defaultPort;

            string portString = ConfigurationManager.AppSettings["server.port"];
            if (int.TryParse(portString, out int port))
            {
                serverPort = port;
            }


            log.Info($"Server IP: {serverIP}");
            log.Info($"Server Port: {serverPort}");

            IServices server = new ServicesJsonProxy(serverIP, serverPort);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#pragma warning disable CA1416 // Validate platform compatibility
            Application.Run(new LoginForm(server));
#pragma warning restore CA1416 // Validate platform compatibility


            log.Info("Closing application...");
        }
    }
}

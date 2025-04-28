using log4net;
using System.IO;
using System.Net.Sockets;
using System.Net;



namespace Networking.Utils
{
    public abstract class AbstractServer
    {
        private readonly int _port;
        private TcpListener _server = null;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AbstractServer));
        protected AbstractServer(int port)
        {
            _port = port;
        }

        public void Start()
        {
            try
            {
                logger.Info($"Starting server on port {_port}");
                _server = new TcpListener(IPAddress.Any, _port);
                _server.Start();
                logger.Info($"Started server on port {_port}");

                while (true)
                {
                    logger.Info("Waiting for clients ...");
                    TcpClient client = _server.AcceptTcpClient();
                    logger.Info("Client connected ...");
                    ProcessRequest(client);
                }
            }
            catch (IOException e)
            {
                logger.Error("Starting server error");
                throw new ServerException("Starting server error", e);
            }
            finally
            {
                Stop();
            }
        }

        protected abstract void ProcessRequest(TcpClient client);

        public void Stop()
        {
            try
            {
                if (_server != null)
                {
                    _server.Stop();
                }
            }
            catch (IOException e)
            {
                throw new ServerException("Closing server error", e);
            }
        }
    }

}

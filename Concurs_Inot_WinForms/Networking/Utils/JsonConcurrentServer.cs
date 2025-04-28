using Networking.JsonProtocol;
using Service.Interfaces;
using log4net;
using System.Net.Sockets;
using System.Threading;

namespace Networking.Utils
{
    public class JsonConcurrentServer : AbsConcurrentServer
    {
        private readonly IServices _services;
        private static readonly ILog logger = LogManager.GetLogger(typeof(JsonConcurrentServer));

        public JsonConcurrentServer(int port, IServices services) : base(port)
        {
            _services = services;
            logger.Info("JsonConcurrentServer started");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            var worker = new ClientJsonWorker(_services, client);
            Thread workerThread = new Thread(new ThreadStart(worker.Run));
            return workerThread;
        }
    }
}

using log4net;
using System.Net.Sockets;
using System.Threading;

namespace Networking.Utils
{
    public abstract class AbsConcurrentServer : AbstractServer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AbsConcurrentServer));
        protected AbsConcurrentServer(int port) : base(port)
        {
            logger.Debug("Concurrent AbstractServer");
        }

        protected override void ProcessRequest(TcpClient client)
        {
            Thread workerThread = CreateWorker(client);
            workerThread.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}

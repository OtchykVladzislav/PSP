using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class ServerReceiver
    {
        private readonly string _address;
        private readonly int _port;

        public ServerReceiver(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Start()
        {
            IPAddress ipAddress = null;
            TcpListener listener = null;

            try
            {
                ipAddress = IPAddress.Parse(_address);

                listener = new TcpListener(ipAddress, _port);
                listener.Start();

                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    var sender = new ServerSender(client);

                    Thread clientThread = new Thread(new ThreadStart(sender.Start));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
    }
}

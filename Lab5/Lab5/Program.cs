using System.Net;
using System.Net.Sockets;

namespace Lab5
{
    class Program
    {
        private const string Address = "127.0.0.1";
        private const int Port = 8080;

        public static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Parse(Address), Port);

            listener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var handler = new RequestHandler(client);
                ThreadPool.QueueUserWorkItem(handler.ProcessRequest);
            }
        }
    }
}

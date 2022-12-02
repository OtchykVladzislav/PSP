using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Lab5
{
    class Program
    {
        private const string CertificatePath = "output.cer";
        private const int Port = 8080;
        private const string Password = "12345678";

        private static X509Certificate2 _serverCertificate;

        public static void Main(string[] args)
        {
            _serverCertificate = new X509Certificate2(CertificatePath, Password);

            var listener = new TcpListener(IPAddress.Any, Port);

            listener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var handler = new RequestHandler(client, _serverCertificate);
                ThreadPool.QueueUserWorkItem(handler.ProcessRequest);
            }
        }
    }
}

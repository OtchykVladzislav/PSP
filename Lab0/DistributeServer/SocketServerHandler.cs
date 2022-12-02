using DistributeServer.Solvers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DistributeServer
{
    public class SocketServerHandler
    {
        private static readonly Encoding ClientEncoding = Encoding.UTF8;

        private readonly string _address;
        private readonly int _port;

        public SocketServerHandler(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Handle()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);

                listenSocket.Listen(10);

                while (true)
                {
                    var handler = listenSocket.Accept();

                    var request = GetRequest(handler);

                    var response = CreateResponse(request);

                    SendResponse(handler, response);

                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private double[,] GetRequest(Socket handler)
        {
            var stringBuilder = new StringBuilder();
            byte[] data = new byte[256];

            do
            {
                var bytes = handler.Receive(data);
                stringBuilder.Append(ClientEncoding.GetString(data, 0, bytes));
            }
            while (handler.Available > 0);

            Console.WriteLine(stringBuilder.ToString());

            var items = stringBuilder.ToString().Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToDouble(x.Replace('.', ','))).ToList();
            var size = Convert.ToInt32(Math.Sqrt(items.Count));

            var matrix = new double[size, size];
            int index = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++, index++)
                {
                    matrix[i, j] = items[index];
                }
            }

            return matrix;
        }

        private string CreateResponse(double[,] matrix)
        {
            var result = PowerSolver.Solve(matrix);
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        private void SendResponse(Socket handler, string message)
        {
            var data = ClientEncoding.GetBytes(message);
            handler.Send(data);
        }
    }
}

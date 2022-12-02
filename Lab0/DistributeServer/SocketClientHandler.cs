using DistributeServer.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DistributeServer
{
    public class SocketClientHandler
    {
        private readonly string _address;
        private readonly int _port;

        public SocketClientHandler(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public double[,] Handle(MatrixModel model)
        {
            double[,] response = null;

            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    
                socket.Connect(ipPoint);

                var request = CreateRequest(model);
                SendRequest(socket, request);

                response = GetResponse(socket);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        private string CreateRequest(MatrixModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        private void SendRequest(Socket socket, string request)
        {
            byte[] data = Encoding.Unicode.GetBytes(request);
            socket.Send(data);
        }

        private double[,] GetResponse(Socket socket)
        {
            var data = new byte[256]; // буфер для ответа
            var stringBuilder = new StringBuilder();

            do
            {
                var bytes = socket.Receive(data, data.Length, 0);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);

            return JsonConvert.DeserializeObject<double[,]>(stringBuilder.ToString());
        }
    }
}

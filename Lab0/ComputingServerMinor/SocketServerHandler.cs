using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ComputingServerMinor
{
    public class SocketServerHandler
    {
        private readonly string _address;
        private readonly int _port;

        public SocketServerHandler(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Handle()
        {
            while (true)
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);

                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    listenSocket.Bind(ipPoint);

                    listenSocket.Listen(10);

                    Console.WriteLine($"Server is launch ({_address}; {_port}). Awaiting connections...\n");

                    while (true)
                    {
                        var handler = listenSocket.Accept();

                        var request = GetRequest(handler);
                        Console.WriteLine("Start working.");

                        var response = CreateResponse(request);

                        for (int i = 0; i < response.GetLength(0); i++)
                        {
                            for (int j = 0; j < response.GetLength(1); j++)
                            {
                                Console.Write($"{response[i, j]}  ");
                            }
                            Console.WriteLine();
                        }

                        SendResponse(handler, response);
                        Console.WriteLine("Finished working.");

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
        }

        private MatrixModel GetRequest(Socket handler)
        {
            var stringBuilder = new StringBuilder();
            byte[] data = new byte[256];

            do
            {
                var bytes = handler.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (handler.Available > 0);

            return JsonConvert.DeserializeObject<MatrixModel>(stringBuilder.ToString());
        }

        private double[,] CreateResponse(MatrixModel matrixModel)
        {
            var resolver = new MinorResolver(matrixModel.Matrix);
            var result = resolver.Resolve(matrixModel.StartRow, matrixModel.EndRow);

            return result;
        }

        private void SendResponse(Socket handler, double[,] response)
        {
            var jsonData = JsonConvert.SerializeObject(response);
            var data = Encoding.Unicode.GetBytes(jsonData);
            handler.Send(data);
        }
    }
}

using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Client
    {
        private readonly string _address;
        private readonly int _port;
        private readonly string _clientName;

        public Client(string address, int port, string clientName)
        {
            _address = address;
            _port = port;
            _clientName = clientName;
        }

        public void Start()
        {
            TcpClient client = null;
            NetworkStream stream = null;

            try
            {
                while (true)
                {
                    client = new TcpClient(_address, _port);
                    stream = client.GetStream();

                    Console.Write("Press enter for send message: ");
                    Console.ReadLine();

                    Console.WriteLine("\nRequest started.");
                    
                    var a = InputMatrix();
                    var b = InputMatrix();
                   

                    var request = new RequestModel
                    {
                        A = a,
                        B = b
                    };

                    request.ClientName = _clientName;
                    var jsonRequest = JsonConvert.SerializeObject(request);

                    PrintRequest(request);

                    var data = Encoding.Unicode.GetBytes(jsonRequest);
                    stream.Write(data, 0, data.Length);

                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    var response = JsonConvert.DeserializeObject<ResponseModel>(builder.ToString());

                    PrintResponse(response);
                    Console.WriteLine("\nRequest finished.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }

                if (client != null)
                {
                    client.Close();
                }
            }
        }

        private void PrintRequest(RequestModel model)
        {
            Console.WriteLine("\nA matrix:");
            PrintMatrix(model.A);

            Console.WriteLine("\nB matrix:");
            PrintMatrix(model.B);
        }

        private void PrintResponse(ResponseModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Message))
            {
                Console.WriteLine($"Server answered with error: {model.Message}");
            }
            else
            {
                PrintMatrix(model.Matrix);
            }
        }

        private void PrintMatrix(int[,] matrix)
        {
            var len = 10;
            var sb = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var item = matrix[i, j].ToString();
                    var itemLen = item.Length;

                    while (itemLen < len)
                    {
                        item += " ";
                        itemLen++;
                    }

                    sb.Append(item);
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        private static int[,] InputMatrix()
        {
            int m, n;
            Console.WriteLine("\nВведите размеры матрицы: ");

            Console.Write("\nКоличество строк : ");
            m = int.Parse(Console.ReadLine());
            if (m == 0)
            {
                Console.WriteLine("Количество строк равна нулю! Завершение программы...");
            }

            Console.Write("\nКоличество столбцов : ");
            n = int.Parse(Console.ReadLine());
            if (n == 0)
            {
                Console.WriteLine("Количество столбцов равна нулю! Завершение программы...");
            }

            int[,] a = new int[m, n];

            Console.WriteLine("\nВведите элементы матрицы через пробел построчно: ");
            for (int i = 0; i < m; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++)
                    a[i, j] = Int32.Parse(s[j]);
            }

            return a;
        }
    }
}

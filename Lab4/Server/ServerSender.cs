using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ServerSender
    {
        private readonly TcpClient _client;
        private NetworkStream _stream;

        public ServerSender(TcpClient client)
        {
            _client = client;
        }

        public void Start()
        {
            try
            {
                _stream = _client.GetStream();

                var requestModel = GetModelFromStream();
                var responseModel = new ResponseModel();

                Console.WriteLine($"\nClient request started. (Client : {requestModel.ClientName})");

                try
                {
                    responseModel.Matrix = MatrixSolver.Multiple(requestModel.A, requestModel.B);
                }
                catch (Exception ex)
                {
                    responseModel.Message = ex.Message;
                }

                var result = JsonConvert.SerializeObject(responseModel);
                var bytes = Encoding.Unicode.GetBytes(result);

                _stream.Write(bytes, 0, bytes.Length);

                Console.WriteLine($"Client request finished. (Client : {requestModel.ClientName})");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_client != null)
                {
                    _client.Close();
                }

                if (_stream != null)
                {
                    _stream.Close();
                }
            }
        }

        private RequestModel GetModelFromStream()
        {
            var builder = new StringBuilder();
            var data = new byte[256];

            do
            {
                var bytes = _stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

            }
            while (_stream.DataAvailable);

            return JsonConvert.DeserializeObject<RequestModel>(builder.ToString());
        }
    }
}

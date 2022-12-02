using Lab5.Pages;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5
{
    public class RequestHandler
    {
        private TcpClient _client;

        public RequestHandler(TcpClient client)
        {
            _client = client;
        }

        public void ProcessRequest(object state)
        {
            var clientStream = _client.GetStream();

            clientStream.ReadTimeout = 200;
            clientStream.WriteTimeout = 200;

            var request = ReadMessage(clientStream);

            Console.WriteLine(request);

            Page responsePage;

            try
            {
                if (request.StartsWith("POST"))
                {
                    Regex regex = new Regex("\r\n\r\n");
                    string[] requestItems = regex.Split(request, 2);

                    Console.WriteLine(requestItems[1]);

                    try
                    {
                        var bodyItem = requestItems[1].Split("=").Last();
                        _ = double.TryParse(bodyItem, out double res);
                        var sin = MyMath.GetSin(res);

                        responsePage = new ResultPage(bodyItem, sin.ToString());
                    }
                    catch
                    {
                        responsePage = new BadPage();
                    }
                }
                else
                {
                    responsePage = new IndexPage();
                }
            }
            catch
            {
                responsePage = new BadPage();
            }

            byte[] response = Encoding.UTF8.GetBytes(responsePage.View());
            clientStream.Write(response, 0, response.Length);
            clientStream.Flush();

            clientStream.Close();
            _client.Close();
        }

        private string ReadMessage(NetworkStream clientStream)
        {
            StringBuilder messageData = new StringBuilder();
            try
            {
                byte[] buffer = new byte[2048];
                int bytes = -1;

                do
                {
                    bytes = clientStream.Read(buffer, 0, buffer.Length);
                    Decoder decoder = Encoding.UTF8.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                    decoder.GetChars(buffer, 0, bytes, chars, 0);
                    messageData.Append(chars);
                }
                while (bytes != 0);
            }
            catch (Exception)
            {

            }

            return messageData.ToString();
        }
    }
}

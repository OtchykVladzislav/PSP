namespace Server
{
    public class Program
    {
        private const string Address = "127.0.0.1";
        private const int Port = 8000;

        public static void Main(string[] args)
        {
            var server = new ServerReceiver(Address, Port);
            server.Start();
        }
    }
}
namespace Client
{
    public class Program
    {
        private const string Address = "127.0.0.1";
        private const int Port = 8000;

        public static void Main(string[] args)
        {
            Console.Write("Enter client name: ");
            var clientName = Console.ReadLine();

            var client = new Client(Address, Port, clientName);
            client.Start();
        }
    }
}
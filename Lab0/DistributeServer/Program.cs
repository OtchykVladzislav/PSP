namespace DistributeServer
{
    public class Program
    {
        private const string ServerConfigPath = "config.txt";
        private const string ComputingConfigPath = "computingconfig.txt";

        public static void Main(string[] args)
        {
            var serverConfig = ConfigReader.GetConfig(ServerConfigPath);
            var computingConfig = ConfigReader.GetComputingConfig(ComputingConfigPath);

            var serverHandler = new SocketServerHandler(serverConfig.Address, serverConfig.Port);

            ClientHandlers.Clients = computingConfig.Select(x => new SocketClientHandler(x.Address, x.Port)).ToList();

            serverHandler.Handle();
        }
    }
}
namespace DistributeServer
{
    public class ConfigReader
    {
        public static Config GetConfig(string path)
        {
            var lines = File.ReadAllLines(path).Select(x => x.Trim());

            return new Config
            {
                Address = lines.Last(),
                Port = Convert.ToInt32(lines.First())
            };
        }

        public static IEnumerable<Config> GetComputingConfig(string path)
        {
            var configs = new List<Config>();
            var lines = File.ReadAllLines(path).Select(x => x.Trim()).ToList();

            for (int i = 0; i < lines.Count - 1; i += 2)
            {
                configs.Add(new Config
                {
                    Port = Convert.ToInt32(lines[i]),
                    Address = lines[i + 1]
                });
            }

            return configs;
        }

        public class Config
        {
            public string Address { get; set; }

            public int Port { get; set; }
        }
    }
}

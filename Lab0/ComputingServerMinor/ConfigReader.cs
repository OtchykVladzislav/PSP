namespace ComputingServerMinor
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

        public class Config
        {
            public string Address { get; set; }

            public int Port { get; set; }
        }
    }
}

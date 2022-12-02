namespace Client
{
    public class RequestModel
    {
        public string ClientName { get; set; }

        public int[,] A { get; set; }

        public int[,] B { get; set; }
    }

    public class ResponseModel
    {
        public int[,] Matrix { get; set; }

        public string Message { get; set; }
    }
}

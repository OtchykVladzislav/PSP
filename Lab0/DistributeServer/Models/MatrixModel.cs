namespace DistributeServer.Models
{
    public class MatrixModel
    {
        public double[,] Matrix { get; set; }

        public int StartRow { get; set; }

        public int EndRow { get; set; }
    }
}

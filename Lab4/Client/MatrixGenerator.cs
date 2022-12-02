namespace Client
{
    public static class MatrixGenerator
    {
        private static readonly Random Random = new Random();

        public const int Min = 5;
        public const int Max = 10;

        public const int MinValue = -30;
        public const int MaxValue = 50;

        public static RequestModel Generate()
        {
            var general = Random.Next(Min, Max);
            var aRows = Random.Next(Min, Max);
            var bColumns = Random.Next(Min, Max);

            var a = FillMatrix(aRows, general);
            var b = FillMatrix(general, bColumns);

            return new RequestModel
            {
                A = a,
                B = b
            };
        }

        private static int[,] FillMatrix(int row, int column)
        {
            var matrix = new int[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = Random.Next(MinValue, MaxValue);
                }
            }

            return matrix;
        }
    }
}

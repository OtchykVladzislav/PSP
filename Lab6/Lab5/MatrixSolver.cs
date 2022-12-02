namespace Lab5
{
    public static class MatrixSolver
    {
        public static double[,] Multiple(double[,] a, double[,] b)
        {
            if (a == null || b == null)
            {
                throw new ArgumentNullException("First or second matrix is null.");
            }

            if (a.GetLength(1) != b.GetLength(0))
            {
                throw new ArgumentException("Matrixs is not valid.");
            }

            return MultipleInternal(a, b);
        }

        private static double[,] MultipleInternal(double[,] a, double[,] b)
        {
            var matrix = new double[a.GetLength(0), b.GetLength(1)];

            for (var i = 0; i < a.GetLength(0); i++)
            {
                for (var j = 0; j < b.GetLength(1); j++)
                {
                    matrix[i, j] = 0;

                    for (var k = 0; k < a.GetLength(1); k++)
                    {
                        matrix[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return matrix;
        }
    }
}

using Accord.Math;

namespace ComputingServerMinor
{
    public class MinorResolver
    {
        private readonly double[,] _matrix;

        public MinorResolver(double[,] matrix)
        {
            _matrix = matrix;
        }

        public double[,] Resolve(int startRow, int endRow) // include only start!
        {
            var size = _matrix.GetLength(0);
            var minorMatrix = new double[size, size];

            for (int i = startRow; i < endRow; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    minorMatrix[i, j] = GetMinor(i, j);
                }
            }

            return minorMatrix;
        }

        private double GetMinor(int row, int column)
        {
            var size = _matrix.GetLength(0);
            var result = new double[size - 1, size - 1];

            int m = 0;
            int k = 0;

            for (int i = 0; i < size; i++)
            {
                if (i == row)
                {
                    continue;
                }

                k = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }

                    result[m, k++] = _matrix[i, j];
                }

                m++;
            }

            return result.Determinant();
        }
    }
}

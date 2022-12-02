using System.Text;

namespace Lab5
{
    public static class MatrixConverter
    {
        public static string ToString(double[,] matrix)
        {
            int len = 5;
            var builder = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var itemLen = matrix[i, j].ToString().Length;
                    var sLen = len - itemLen;

                    var spasing = "";
                    for (int k = 0; k < sLen; k++)
                    {
                        spasing += " ";
                    }

                    builder.Append($"{matrix[i, j]}{spasing}");
                }

                builder.Append("</br>");
            }

            return builder.ToString();
        }

        public static bool TryGetMatrixs(string message, out double[,] a, out double[,] b)
        {
            var result = true;

            try
            {
                var items = message.Split('&').ToList();
                var matrixLength = items.Count / 2;
                var aItems = items.Take(matrixLength);
                var bItems = items.TakeLast(matrixLength);

                var matrixSize = (int)Math.Sqrt(matrixLength);

                a = GetMatrix(aItems, matrixSize);
                b = GetMatrix(bItems, matrixSize);
            }
            catch
            {
                a = null;
                b = null;
                result = false;  
            }

            return result;
        }

        private static double[,] GetMatrix(IEnumerable<string> items, int matrixSize)
        {
            var matrix = new double[matrixSize, matrixSize];

            foreach (var item in items)
            {
                var symbols = item.Split('_', '=').ToArray();
                int i = Convert.ToInt32(symbols[1]);
                int j = Convert.ToInt32(symbols[2]);

                var stringValues = item.Split('=').ToArray();
                double value = Convert.ToDouble(stringValues[1]);

                matrix[i, j] = value;
            }

            return matrix;
        }
    }
}

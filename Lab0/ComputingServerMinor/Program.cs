namespace ComputingServerMinor
{
    public class Program
    {
        private const string ConfigPath = "config.txt";

        public static void Main(string[] args)
        {
            var config = ConfigReader.GetConfig(ConfigPath);
            var handler = new SocketServerHandler(config.Address, config.Port);

            handler.Handle();
        }
    }
}

//var matrix = new double[4, 4] { { 2, 5, 7, 1 }, { 6, 3, 4, 1 }, { 5, -2, -3, 1 }, { 1, 1, 2, 0 } };

//var det = matrix.Determinant();
//var minorMatrix = GetMinorMatrix(matrix);
//var additions = minorMatrix.CreateAdditions();
//additions = additions.Transpose();

//for (int i = 0; i < additions.GetLength(0); i++)
//{
//    for (int j = 0; j < additions.GetLength(1); j++)
//    {
//        additions[i, j] = additions[i, j] / det;
//        Console.Write($"{additions[i, j]}  ");
//    }
//    Console.WriteLine();
//}

//static double[,] GetMinorMatrix(double[,] matrix)
//{
//    var size = matrix.GetLength(0);
//    var minorMatrix = new double[size, size];

//    for (int i = 0; i < size; i++)
//    {
//        for (int j = 0; j < size; j++)
//        {
//            minorMatrix[i, j] = GetMinor(matrix, i, j);
//        }
//    }

//    return minorMatrix;
//}

//static double GetMinor(double[,] matrix, int row, int column)
//{
//    var size = matrix.GetLength(0); 
//    var result = new double[size - 1, size - 1];

//    int m = 0;
//    int k = 0;

//    for (int i = 0; i < size; i++)
//    {
//        if (i == row)
//        {
//            continue;
//        }

//        k = 0;
//        for (int j = 0; j < size; j++)
//        {
//            if (j == column)
//            {
//                continue;
//            }

//            result[m, k++] = matrix[i, j];
//        }

//        m++;
//    }
//    return result.Determinant();
//}

//public static class MatrixExtensions
//{
//    public static double[,] CreateAdditions(this double[,] matrix)
//    {
//        var size = matrix.GetLength(0);
//        var result = new double[size, size];

//        for (int i = 0; i < size; i++)
//        {
//            for (int j = 0; j < size; j++)
//            {
//                result[i, j] = Math.Pow((-1), i + j) * matrix[i, j];
//            }
//        }

//        return result;
//    }
//}
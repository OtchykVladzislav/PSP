using Accord.Math;
using DistributeServer.Models;

namespace DistributeServer.Solvers
{
    public class PowerSolver
    {
        private static double[,] _matrix;

        public static double[,] Solve(double[,] matrix)
        {
            var det = matrix.Determinant();

            if (det == 0)
            {
                throw new ArgumentException("Determinant can not be null.");
            }

            _matrix = new double[matrix.GetLength(0), matrix.GetLength(0)];

            var rows = matrix.GetLength(0);
            var countHandlers = ClientHandlers.Clients.Count;

            if (countHandlers > 1)
            {
                var step = rows / countHandlers;

                var start = 0;
                var end = step;

                Thread[] threads = new Thread[countHandlers];

                for (int i = 0; i < countHandlers; i++)
                {
                    threads[i] = new Thread(arg =>
                    {
                        var args = arg as MatrixThreadModel;
                        SolveInternal(args.ClientHandler, args.Matrix, args.StartRow, args.EndRow);
                    });

                    threads[i].Start(new MatrixThreadModel { ClientHandler = ClientHandlers.Clients[i], Matrix = matrix, StartRow = start, EndRow = end });

                    start = end;
                    end = i == countHandlers - 2 ? rows : end + step;
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }
            }
            else
            {
                var thread = new Thread(arg =>
                {
                    var args = arg as MatrixThreadModel;
                    SolveInternal(args.ClientHandler, args.Matrix, args.StartRow, args.EndRow);
                });

                thread.Start(new MatrixThreadModel { ClientHandler = ClientHandlers.Clients.First(), Matrix = matrix, StartRow = 0, EndRow = rows });

                thread.Join();
            }

            var result = _matrix.CreateAdditions();
            result = result.Transpose();

            return result;
        }

        private static void SolveInternal(SocketClientHandler handler, double[,] matrix, int startRow, int endRow)
        {
            var result = handler.Handle(new MatrixModel { Matrix = matrix, StartRow = startRow, EndRow = endRow });

            for (int i = startRow; i < endRow; i++)
            {
                for (int j = 0; j < _matrix.GetLength(0); j++)
                {
                    _matrix[i, j] = result[i, j];
                }
            }
        }
    }
}

using System;
using System.Threading;
using System.Diagnostics;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}.");

            var matrixA = new Matrix(new int[,] { 
                { 1, -1, 5, 7, 7, 8, 9 }, 
                { 1, -1, 5, 7, 7, 8, 9 }, 
                { 1, -1, 5, 7, 7, 8, 9 }, 
                { 1, -1, 5, 7, 7, 8, 9 } });

            var matrixB = new Matrix(new int[,] { 
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 },
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 },
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 },
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 },
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 },
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 },
                { 1, 1, 5, 8, 7, 15, 254, 47, 7, 8 }});

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var resultMatrix = Matrix.Multiple(matrixA, matrixB);
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time in milliseconds: {stopwatch.ElapsedMilliseconds}");

            Matrix.PrintMatrix(resultMatrix);

            Console.WriteLine("\n\nMultiple matrices by using Parallel.");
            stopwatch.Reset();
            stopwatch.Start();
            resultMatrix = Matrix.MultipleUsingParallel(matrixA,matrixB);
            stopwatch.Stop();

            Console.WriteLine($"Elapsed time in milliseconds for parallel execution: {stopwatch.ElapsedMilliseconds}");

            Matrix.PrintMatrix(resultMatrix);

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}

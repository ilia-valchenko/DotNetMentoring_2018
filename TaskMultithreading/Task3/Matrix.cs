using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    /// <summary>
    /// The integer matrix.
    /// </summary>
    public class Matrix
    {
        private int[,] _array;

        /// <summary>
        /// Initilializes an instance of the <see cref="Matrix"/> class.
        /// </summary>
        public Matrix(): this(new int[0 ,0]) {}

        /// <summary>
        /// Initializes an instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public Matrix(int[,] array)
        {
            _array = array;
        }

        /// <summary>
        /// Multiples two matrixes.
        /// </summary>
        /// <param name="matrixA">The matrix A.</param>
        /// <param name="matrixB">The matrix B.</param>
        /// <returns>The matrix C as a result of multiplication.</returns>
        public static Matrix Multiple(Matrix matrixA, Matrix matrixB)
        {
            int numberOfRowsOfMatrixA = matrixA._array.GetUpperBound(0) + 1;
            int numberOfRowsOfMatrixB = matrixB._array.GetUpperBound(0) + 1;
            
            if(numberOfRowsOfMatrixA == 0)
            {
                throw new ArgumentException("The matrix A is empty.");
            }

            if(numberOfRowsOfMatrixB == 0)
            {
                throw new ArgumentException("The matrix B is empty.");
            }

            int numberOfColumnsOfMatrixA = matrixA._array.Length / numberOfRowsOfMatrixA;
            int numberOfColumnsOfMatrixB = matrixB._array.Length / numberOfRowsOfMatrixB;

            if (numberOfColumnsOfMatrixA != numberOfRowsOfMatrixB)
            {
                throw new ArgumentException("The number of columns of matrix A is not equal to the number of rows of matrix B.");
            }

            int numberOfRowsOfMatrixC = numberOfRowsOfMatrixA;
            int numberOfColumnsOfMatrixC = numberOfColumnsOfMatrixB;
            var arrayForResultMatrix = new int[numberOfRowsOfMatrixC, numberOfColumnsOfMatrixC];

            for (int i = 0; i < numberOfRowsOfMatrixC; i++)
            {
                for (int j = 0; j < numberOfColumnsOfMatrixC; j++)
                {
                    arrayForResultMatrix[i, j] = 0;

                    for (int k = 0; k < numberOfColumnsOfMatrixA; k++)
                    {
                        Thread.Sleep(50);
                        arrayForResultMatrix[i, j] += matrixA._array[i, k] * matrixB._array[k, j];
                    }
                }
            }

            return new Matrix(arrayForResultMatrix);
        }

        /// <summary>
        /// Multiples two matrixes by using Parallel.
        /// </summary>
        /// <param name="matrixA">The matrix A.</param>
        /// <param name="matrixB">The matrix B.</param>
        /// <returns>The matrix C as a result of multiplication.</returns>
        public static Matrix MultipleUsingParallel(Matrix matrixA, Matrix matrixB)
        {
            int numberOfRowsOfMatrixA = matrixA._array.GetUpperBound(0) + 1;
            int numberOfRowsOfMatrixB = matrixB._array.GetUpperBound(0) + 1;

            if (numberOfRowsOfMatrixA == 0)
            {
                throw new ArgumentException("The matrix A is empty.");
            }

            if (numberOfRowsOfMatrixB == 0)
            {
                throw new ArgumentException("The matrix B is empty.");
            }

            int numberOfColumnsOfMatrixA = matrixA._array.Length / numberOfRowsOfMatrixA;
            int numberOfColumnsOfMatrixB = matrixB._array.Length / numberOfRowsOfMatrixB;

            if (numberOfColumnsOfMatrixA != numberOfRowsOfMatrixB)
            {
                throw new ArgumentException("The number of columns of matrix A is not equal to the number of rows of matrix B.");
            }

            int numberOfRowsOfMatrixC = numberOfRowsOfMatrixA;
            int numberOfColumnsOfMatrixC = numberOfColumnsOfMatrixB;
            var arrayForResultMatrix = new int[numberOfRowsOfMatrixC, numberOfColumnsOfMatrixC];

            Parallel.For(0, numberOfRowsOfMatrixC, i =>
            {
                for (int j = 0; j < numberOfColumnsOfMatrixC; j++)
                {
                    arrayForResultMatrix[i, j] = 0;

                    for (int k = 0; k < numberOfColumnsOfMatrixA; k++)
                    {
                        Thread.Sleep(50);
                        arrayForResultMatrix[i, j] += matrixA._array[i, k] * matrixB._array[k, j];
                    }
                }
            });

            return new Matrix(arrayForResultMatrix);
        }

        /// <summary>
        /// Prints matrix to console.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public static void PrintMatrix(Matrix matrix)
        {
            int numberOfRows = matrix._array.GetUpperBound(0) + 1;

            if (numberOfRows == 0)
            {
                Console.WriteLine("The matrix is empty.");
            }
            else
            {
                int numberOfColumns = matrix._array.Length / numberOfRows;

                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        Console.Write($"{matrix._array[i, j]} \t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

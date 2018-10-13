using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MathHelperLibrary;
using InfrastructureLibrary;

namespace Task2
{
    class Program
    {
        private const int MinRandomValue = 1;
        private const int MaxRandomValue = 10;
        private const int CountOfRandomIntegers = 15;
        private const int MultiplikatorMinValue = 1;
        private const int MultiplikatorMaxValue = 5;

        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            var generateRandomIntegersTask = new Task<int[]>(() =>
            {
                var randomIntegers = MathHelper.GenerateRandomIntegers(MinRandomValue, MaxRandomValue, CountOfRandomIntegers);

                Console.WriteLine("Generated random integers:");
                Infrastructure.PrintElementsOfCollection<int>(randomIntegers);

                return randomIntegers;
            });

            generateRandomIntegersTask
                .ContinueWith(data =>
                {
                    var multiplikator = MathHelper.GenerateRandomInteger(MultiplikatorMinValue, MultiplikatorMaxValue);

                    Console.WriteLine($"Multiple each element of the array by {multiplikator}.");

                    MultipleEachElementOfIntegerArrayBy(data.Result, multiplikator);

                    Console.WriteLine("The result of multiplication:");
                    Infrastructure.PrintElementsOfCollection<int>(data.Result);

                    return data.Result;
                })
            .ContinueWith(data =>
            {
                Array.Sort(data.Result);

                Console.WriteLine("Sorted array:");
                Infrastructure.PrintElementsOfCollection<int>(data.Result);

                return data.Result;
            })
            .ContinueWith(data =>
            {
                var averageValue = data.Result.Average();

                Console.WriteLine($"The average value: {averageValue}.");
            });

            generateRandomIntegersTask.Start();

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Multiples each element of an array of interger by another integer.
        /// </summary>
        /// <param name="array">The array of integers.</param>
        /// <param name="multiplikator">The integer value.</param>
        private static void MultipleEachElementOfIntegerArrayBy(int[] array, int multiplikator)
        {
            if(array.Length == 0)
            {
                throw new ArgumentException("The array is empty.");
            }

            for(int i = 0; i < array.Length; i++)
            {
                array[i] *= multiplikator;
            }
        }
    }
}

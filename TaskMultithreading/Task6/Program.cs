using System;
using System.Collections.Generic;
using System.Threading;
using MathHelperLibrary;
using InfrastructureLibrary;

namespace Task6
{
    class Program
    {
        private const int NumberOfNewElements = 10;
        private const int MinRandomValue = 1;
        private const int MaxRandomValue = 10;

        private static AutoResetEvent[] waitHandles = new AutoResetEvent[2];
        private static Thread[] threads = new Thread[2];

        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            var listOfIntegers = new List<int>();

            waitHandles[0] = new AutoResetEvent(false);
            waitHandles[1] = new AutoResetEvent(false);

            threads[0] = new Thread(FillCollectionByRandomIntegers);
            threads[1] = new Thread(() => {
                for(int i = 0; i < NumberOfNewElements; i++)
                {
                    waitHandles[1].WaitOne();
                    Infrastructure.PrintElementsOfCollection<int>(listOfIntegers);
                    waitHandles[0].Set();
                }
            });

            threads[0].Start(listOfIntegers);
            threads[1].Start();

            waitHandles[0].Set();

            Console.WriteLine($"\n\nThe end of the Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine("Tap to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Fills a collection by random integers.
        /// </summary>
        /// <param name="collection">The collection.</param>
        private static void FillCollectionByRandomIntegers(object state)
        {
            Console.WriteLine($"Filling the collection... ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            ICollection<int> collection = default(ICollection<int>);

            try
            {
                collection = (ICollection<int>)state;
            }
            catch(InvalidCastException castException)
            {
                Console.WriteLine(castException.Message);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var randomIntegers = MathHelper.GenerateRandomIntegers(MinRandomValue, MaxRandomValue, NumberOfNewElements);

            foreach(var item in randomIntegers)
            {
                waitHandles[0].WaitOne();
                collection.Add(item);
                waitHandles[1].Set();
            }
        }
    }
}

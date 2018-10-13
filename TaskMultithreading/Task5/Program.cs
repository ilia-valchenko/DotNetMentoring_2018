using System;
using System.Threading;

namespace Task5
{
    class Program
    {
        private const int InitialCount = 1;
        private const int MaxCount = 1;

        private static int counter = 10;
        private static Semaphore semaphore = new Semaphore(InitialCount, MaxCount);

        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            ThreadPool.QueueUserWorkItem(DecrementGlobalCounter);

            Console.WriteLine($"The end of the Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Decrements global counter.
        /// </summary>
        public static void DecrementGlobalCounter(object state)
        {
            semaphore.WaitOne();

            if (counter >= 0)
            {
                Console.WriteLine($"Counter = {counter}. ThreadId = {Thread.CurrentThread.ManagedThreadId}");
                counter--;

                ThreadPool.QueueUserWorkItem(DecrementGlobalCounter);
            }
            else
            {
                Console.WriteLine("Counter is less then zero or equal to it.");
            }

            semaphore.Release();
        }
    }
}

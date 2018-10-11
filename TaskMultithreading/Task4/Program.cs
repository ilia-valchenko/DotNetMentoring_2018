using System;
using System.Threading;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            int counter = 10;

            DecrementCounter(counter);

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }

        public static void DecrementCounter(object state)
        {
            int counter = (int)state;

            if (counter >= 0)
            {
                Console.WriteLine($"Counter = {counter}. ThreadId = {Thread.CurrentThread.ManagedThreadId}");
                counter--;
                var t = new Thread(DecrementCounter);
                t.Start(counter);
                t.Join();
            }
            else
            {
                Console.WriteLine("Counter is less then zero or equal to it.");
            }
        }
    }
}

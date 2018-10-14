using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}.");

            var parentTask = new Task(DoSomething);
            parentTask.ContinueWith(pt =>
            {
                // Case B
                Console.WriteLine($"Error has occurred.\nErrorMessage: {pt.Exception.Message}");
            },
            TaskContinuationOptions.OnlyOnFaulted)
            .ContinueWith(pt =>
            {
                // Case A
                Console.WriteLine("Continue with other task.");
            })
            .ContinueWith(pt =>
            {
                // Case C
                Console.WriteLine("Сontinue on the same thread as parent task is executed to.");
            },
            TaskContinuationOptions.ExecuteSynchronously)
            .ContinueWith(pt =>
            {
                // Case D
                Console.WriteLine($"Contibue outside of thread pool.");
            },
            TaskScheduler.FromCurrentSynchronizationContext());

            parentTask.Start();

            Console.WriteLine($"\n\nTha end of the Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine("Tap to continue...");
            Console.ReadKey();
        }

        private static void DoSomething()
        {
            Console.WriteLine("I am doing something...");
            throw new Exception("Fake exception");
            Thread.Sleep(2000);
            Console.WriteLine("I finished doing something.");
        }
    }
}

using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task1
{
    class Program
    {
        private const int NumberOfTasks = 100;
        private const int IterationLimit = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            var parentTask = new Task(() => {
                var childTasks = new Task[NumberOfTasks];
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory(
                    cts.Token,
                    TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.None,
                    TaskScheduler.Default);

                for(int i = 0; i < NumberOfTasks; i++)
                {
                    childTasks[i] = tf.StartNew(() => IterateAndPrint(cts.Token, IterationLimit));
                }

                tf.ContinueWhenAll(childTasks, tasks =>
                {
                    Console.WriteLine($"\nAll {tasks.Length} child tasks finished.");
                });
            });

            parentTask.ContinueWith(p =>
            {
                var sb = new StringBuilder("The following exception(s) occurred:" + Environment.NewLine);

                foreach (var exc in p.Exception.InnerExceptions)
                {
                    sb.AppendLine(" " + exc.GetType().ToString());
                }

                Console.WriteLine(sb.ToString());
            },
            TaskContinuationOptions.OnlyOnFaulted);

            parentTask.Start();

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Iterates from zero to iteration limit and print the number 
        /// of an iteration.
        /// </summary>
        /// <param name="iterationLimit">The iteration limit.</param>
        public static void IterateAndPrint(CancellationToken ct, int iterationLimit)
        {
            for(int i = 0; i <= iterationLimit; i++)
            {
                ct.ThrowIfCancellationRequested();
                Console.WriteLine($"Number of iteration: {i}. ThreadId = {Thread.CurrentThread.ManagedThreadId}.");
            }
        }
    }
}

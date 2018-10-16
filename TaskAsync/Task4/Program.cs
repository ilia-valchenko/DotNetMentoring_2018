using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            StartAsync();

            Console.WriteLine($"The end of Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Tap to continue...");
            Console.ReadKey();
        }

        private static async void StartAsync()
        {
            DoSomething1().GetAwaiter().GetResult();
            DoSomething2().GetAwaiter().GetResult();
            DoSomething3().GetAwaiter().GetResult();
            //await DoSomething4();
        }

        private static async Task DoSomething1()
        {
            await Task.Run(() => {
                Thread.Sleep(3000);
                Console.WriteLine("I did something for #1.");
            });
        }

        private static Task DoSomething2()
        {
            return Task.Run(() => {
                Thread.Sleep(2000);
                Console.WriteLine("I did something for #2.");
            });
        }

        private static Task DoSomething3()
        {
            return Task.Run(() => {
                Thread.Sleep(1000);
                Console.WriteLine("I did something for #3.");
            });
        }

        private static string DoSomething4()
        {
            return "I do someting for #4";
        }
    }
}

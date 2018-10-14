using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        private const string ExitCommand = "exit";
        private static bool exit = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a value of the limit to start sum operation or 'exit' to stop the program:");

            while(!exit)
            {
                StartExecution();
            }

            Console.WriteLine("The 'exit' was requested.");
            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }

        private static async void StartExecution()
        {
            string input = await ReadInputAsync();
            int limitValue;

            if(input == ExitCommand)
            {
                exit = true;
                return;
            } 
            else
            {
                if(Int32.TryParse(input, out limitValue))
                {
                    int result = await SumAsync(limitValue);
                    Console.WriteLine($"Result = {result}. Limit = {limitValue}.");
                }
                else
                {
                    Console.WriteLine("Invalid input. The input is not an integer and is not a boolean.");
                    return;
                }
            }
        }

        /// <summary>
        /// Sums integers from zero to the limit.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <returns>Returns the sum of integers.</returns>
        private static Task<int> SumAsync(int limit)
        {
            return Task<int>.Run(() => {
                int sum = 0;

                for(int i = 1; i < limit; i++)
                {
                    Thread.Sleep(200);
                    sum += i;
                }

                return sum;
            });
        }

        /// <summary>
        /// Reads an input async.
        /// </summary>
        /// <returns>The string representation of an input.</returns>
        private static Task<string> ReadInputAsync()
        {
            return Task<string>.Run(() => Console.ReadLine());
        }
    }
}

using CentralManagementServer.Logger;
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();
            var centralService = new CentralManagementService.CentralManagementService(logger);

            centralService.StartCentralQueueProcessing();

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}

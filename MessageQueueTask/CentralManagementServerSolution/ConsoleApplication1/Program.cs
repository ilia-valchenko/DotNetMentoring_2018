using System;
using CentralManagementServer.Logger;
using System.Threading.Tasks;
using MessageQueueTask.MessagesLibrary.CentralServiceMessages;

namespace ConsoleApplication1
{
    class Program
    {
        private static CentralManagementService.CentralManagementService _centralService;

        static void Main(string[] args)
        {
            var logger = new Logger();
            _centralService = new CentralManagementService.CentralManagementService(logger);

            Console.WriteLine("Start processing central queue.");

            var centralQueueProcessingTask = Task.Run(() => {
                _centralService.StartCentralQueueProcessing();
            });

            Console.WriteLine("Has started processing central queue.");
            Console.WriteLine("Start waiting for inputing broadcast messages.");

            var task2 = Task.Run(() =>
            {
                DoSomething();
            });

            Console.WriteLine("has started waiting for inputing broadcast messages.");

            centralQueueProcessingTask.Wait();
        }

        private static void DoSomething()
        {
            while (true)
            {
                var consoleLine = Console.ReadLine();

                _centralService.SendBroadcastMessage(new TestMessage
                {
                    Text = consoleLine
                });
            }
        }
    }
}

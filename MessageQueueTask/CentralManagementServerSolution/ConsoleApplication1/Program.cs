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

            Task.Run(() =>
            {
                StartSendingBroadcastMessages();
            });

            Console.WriteLine("has started waiting for inputing broadcast messages.");

            centralQueueProcessingTask.Wait();
        }

        private static void StartSendingBroadcastMessages()
        {
            while (true)
            {
                Console.WriteLine("Chose command:\n1. Send broadcast message\n2. Exit");

                var consoleLine = Console.ReadLine();

                switch(consoleLine)
                {
                    case "1":
                        Console.WriteLine("Enter the value of the fake settings:");

                        var fakeSettingsValue = Console.ReadLine();

                        _centralService.SendBroadcastMessage(new TestMessage
                        {
                            Text = fakeSettingsValue
                        });

                        break;

                    case "2":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }
    }
}

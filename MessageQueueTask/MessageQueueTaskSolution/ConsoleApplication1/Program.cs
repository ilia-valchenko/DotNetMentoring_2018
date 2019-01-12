using MessageQueueTask.ImageWatcherService;
using MessageQueueTask.Logger;
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start console application.");

            ILogger logger = new Logger();

            var imageService = new ImageService(logger);
            Console.WriteLine("\nStarting watching images.");
            imageService.StartWatchingImages();
            Console.WriteLine("Has Started watching for images.");

            Console.WriteLine("\nStart listening broadcast messages.");
            imageService.StartListenBroadcastMessagesAsync();
            Console.WriteLine("Has started listening broadcast messages.");

            Console.WriteLine("\nStart sending current status to the central service.");
            imageService.StartSendingCurrentStatusToCentralService();
            Console.WriteLine("Has started sending current status to the central service.");

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}

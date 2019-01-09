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
            Console.WriteLine("Starting watching images.");
            imageService.StartWatchingImages();
            Console.WriteLine("Started watching for images.");

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}

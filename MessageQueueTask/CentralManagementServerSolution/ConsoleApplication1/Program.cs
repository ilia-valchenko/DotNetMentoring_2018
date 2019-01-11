using CentralManagementServer.Logger;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();
            var centralService = new CentralManagementService.CentralManagementService(logger);

            var centralQueueProcessingTask = Task.Run(() => {
                centralService.StartCentralQueueProcessing();
            });

            //var task2 = Task.Run(() => {
            //    DoSomething();
            //});

            centralQueueProcessingTask.Wait();
        }
    }
}

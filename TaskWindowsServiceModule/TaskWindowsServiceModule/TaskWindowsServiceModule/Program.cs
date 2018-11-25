using System.ServiceProcess;

namespace TaskWindowsServiceModule
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new ImagePdfBinderService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}

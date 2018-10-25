using System;

namespace ServerConsoleApplication
{
    class Program
    {
        private const string PipeName = "testpipe";

        static void Main()
        {
            var server = new Server(PipeName);
            server.Start();

            Console.WriteLine("\n\nTap to close the window...");
            Console.ReadKey();
        }
    }
}

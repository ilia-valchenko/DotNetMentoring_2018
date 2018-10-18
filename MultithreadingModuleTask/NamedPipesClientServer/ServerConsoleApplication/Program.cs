namespace ServerConsoleApplication
{
    class Program
    {
        private const string PipeName = "TestPipeName";
        private const string StopCommand = "stop";

        static void Main(string[] args)
        {
            var server = new Server(PipeName);
            server.Start();
        }
    }
}

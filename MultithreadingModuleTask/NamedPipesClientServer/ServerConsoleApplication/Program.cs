using System;
using System.IO;
using System.IO.Pipes;
using System.Collections.Generic;

namespace ServerConsoleApplication
{
    class Program
    {
        private const string PipeName = "TestPipeName";
        private const string StopCommand = "stop";
        private const int NumberOfStoredMessages = 5;

        //private static readonly List<string> storedMessages = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("[SERVER]: Server started.");

            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName))
            {
                Console.WriteLine("[SERVER]: Waiting for a client connection...");

                pipeServer.WaitForConnection();

                Console.WriteLine("[SERVER]: Connection established.");

                using (StreamReader streamReader = new StreamReader(pipeServer))
                {
                    string receivedMessage;

                    while(true)
                    {
                        Console.WriteLine("[SERVER]: Wait for a message to arrive from the cliet...");

                        if (pipeServer.IsConnected)
                        {
                            receivedMessage = streamReader.ReadLine();
                            Console.WriteLine($"[SERVER]: Client's message is '{receivedMessage}'.");
                        }
                        else
                        {
                            Console.WriteLine("[SERVER]: Client disconnected.");
                            pipeServer.Disconnect();

                            Console.WriteLine("[SERVER]: Waiting for a client connection...");
                            pipeServer.WaitForConnection();
                            Console.WriteLine("[SERVER]: Connection established.");
                        }

                        //storedMessages.Add(receivedMessage);
                    }
                }
            }
        }
    }
}

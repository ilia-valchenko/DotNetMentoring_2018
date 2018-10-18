using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

namespace ServerConsoleApplication
{
    public class Server
    {
        private const string StopReadingMessagesCommand = "StopReadingMessagesCommand";
        private const int NumberOfStoredMessages = 5;
        private NamedPipeServerStream pipeServer;
        private readonly List<string> storedMessages = new List<string>();
        private readonly string pipeName;

        public Server(string pipeName)
        {
            this.pipeName = pipeName;
        }

        public void Start()
        {
            Console.WriteLine("[SERVER]: Server started.");

            using (pipeServer = new NamedPipeServerStream(pipeName))
            {
                Console.WriteLine("[SERVER]: Waiting for a client connection...");

                pipeServer.WaitForConnection();

                Console.WriteLine("[SERVER]: Connection established.");

                Console.WriteLine($"[SERVER]: Send last {NumberOfStoredMessages} stored messages to a new client.");

                using (StreamReader streamReader = new StreamReader(pipeServer))
                {
                    string receivedMessage;

                    while (true)
                    {
                        Console.WriteLine("[SERVER]: Wait for a message to arrive from the cliet...");

                        if (pipeServer.IsConnected)
                        {
                            receivedMessage = streamReader.ReadLine();
                            Console.WriteLine($"[SERVER]: Client's message is '{receivedMessage}'.");
                            storedMessages.Add(receivedMessage);
                        }
                        else
                        {
                            Console.WriteLine("[SERVER]: Client disconnected.");
                            pipeServer.Disconnect();

                            Console.WriteLine("[SERVER]: Waiting for a client connection...");
                            pipeServer.WaitForConnection();
                            Console.WriteLine("[SERVER]: Connection established.");
                            Console.WriteLine($"[SERVER]: Send last {NumberOfStoredMessages} stored messages to a new client.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sends first N stored messages to a new client.
        /// </summary>
        /// <param name="pipeServer">The pipe server.</param>
        /// <param name="count">The number of messages.</param>
        public void SendMessagesToClient(int count)
        {
            using (StreamWriter streamWriter = new StreamWriter(pipeServer))
            {
                for (int i = storedMessages.Count - 1; i >= count; i--)
                {
                    streamWriter.WriteLine(storedMessages[i]);
                    streamWriter.Flush();
                }

                streamWriter.WriteLine(StopReadingMessagesCommand);
                streamWriter.Flush();
            }
        }
    }
}

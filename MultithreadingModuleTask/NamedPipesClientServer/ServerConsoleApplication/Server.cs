using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;

namespace ServerConsoleApplication
{
    public class Server
    {
        private const string StopReadingMessagesCommand = "StopReadingMessagesCommand";
        private const int NumberOfStoredMessages = 5;
        private const int NumberOfThreads = 4;

        private Thread[] servers = new Thread[NumberOfThreads];
        private NamedPipeServerStream pipeServer;
        private readonly List<string> storedMessages = new List<string>();
        private readonly string pipeName;

        public Server(string pipeName)
        {
            this.pipeName = pipeName;
        }

        public void Start()
        {
            int i;
            Console.WriteLine("[SERVER]: Server started.");

            for (i = 0; i < NumberOfThreads; i++)
            {
                servers[i] = new Thread(ServerThread);
                servers[i].Start();
            }

            Thread.Sleep(250);

            while (i > 0)
            {
                for (int j = 0; j < NumberOfThreads; j++)
                {
                    if (servers[j] != null)
                    {
                        if (servers[j] != null)
                        {
                            if (servers[j].Join(250))
                            {
                                Console.WriteLine($"[SERVER]: Server thread[{servers[j].ManagedThreadId}] finished.");
                                servers[j] = null;
                                i--; // Decrement the thread watch count.
                            }
                        }
                    }
                }
            }

            Console.WriteLine("[SERVER]: Server threads exhausted, exiting.");
        }

        private void ServerThread(object data)
        {
            using (var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, NumberOfThreads))
            {
                Console.WriteLine("[SERVER]: Waiting for a client connection...");

                // Wait for a client to connect.
                pipeServer.WaitForConnection();

                Console.WriteLine($"[SERVER]: Client connected on thread[{Thread.CurrentThread.ManagedThreadId}].");

                Console.WriteLine($"[SERVER]: Send last {NumberOfStoredMessages} stored messages to a new client.");

                SendMessagesToClient(NumberOfStoredMessages);

                try
                {
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
                catch (Exception exc)
                {
                    Console.WriteLine($"Failed to read messages from client. See the error below.\nErrorMessage: {exc.Message}\nStackTrace: {exc.StackTrace}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Sends first N stored messages to a new client.
        /// </summary>
        /// <param name="count">The number of messages.</param>
        public void SendMessagesToClient(int count)
        {
            if (storedMessages != null && storedMessages.Any())
            {
                try
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
                catch (Exception exc)
                {
                    Console.WriteLine($"Failed to send messages to client. See the error below.\nErrorMessage: {exc.Message}\nStackTrace: {exc.StackTrace}");
                    throw;
                }
            }
        }
    }
}

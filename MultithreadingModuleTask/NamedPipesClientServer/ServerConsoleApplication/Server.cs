using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading;

namespace ServerConsoleApplication
{
    /// <summary>
    /// The server class.
    /// </summary>
    public class Server
    {
        private const string StopReadingMessagesCommand = "StopReadingMessagesCommand";
        private const int NumberOfStoredMessages = 5;
        private const int NumberOfThreads = 4;

        private Thread[] servers = new Thread[NumberOfThreads];
        private NamedPipeServerStream pipeServer;

        private readonly string pipeName;
        private readonly List<string> storedMessages = new List<string>
        {
            "test 1",
            "test 2",
            "test 3",
            "test 4",
            "test 5",
            "test 6",
            "test 7"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="pipeName"></param>
        public Server(string pipeName)
        {
            this.pipeName = pipeName;
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
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
            var pipeServer = new NamedPipeServerStream(this.pipeName, PipeDirection.InOut, NumberOfThreads);

            Console.WriteLine($"Can server read = {pipeServer.CanRead}\nCan server write = {pipeServer.CanRead}");

            // Wait for a client to connect.
            pipeServer.WaitForConnection();

            Console.WriteLine($"[SERVER]: Client connected on thread[{Thread.CurrentThread.ManagedThreadId}].");

            Console.WriteLine($"[SERVER]: Send last {NumberOfStoredMessages} stored messages to a new client. Thread[{Thread.CurrentThread.ManagedThreadId}].");

            if (storedMessages != null && storedMessages.Any())
            {
                try
                {
                    var ss = new StreamString(pipeServer);

                    foreach (string msg in storedMessages)
                    {
                        Console.WriteLine($"[SERVER]: Send '{msg}' to client");

                        ss.WriteString(msg);

                        Console.WriteLine("[SERVER]: Message was successfully delivered.");
                    }

                    ss.WriteString(StopReadingMessagesCommand);
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Failed to send messages to client. See the error below.\nErrorMessage: {exc.Message}\nStackTrace: {exc.StackTrace}");
                    throw;
                }
            }
            else
            {
                Console.WriteLine("[SERVER]: History of messages is empty. Nothing will be send to a client.");
            }

            try
            {
                var ss = new StreamString(pipeServer);
                string receivedMessage = ss.ReadString();

                while (true)
                {
                    if (pipeServer.IsConnected &&
                        receivedMessage != StopReadingMessagesCommand &&
                        !string.IsNullOrEmpty(receivedMessage))
                    {
                        Console.WriteLine($"[SERVER]: Client's message is '{receivedMessage}'. Thread[{Thread.CurrentThread.ManagedThreadId}].");
                        storedMessages.Add(receivedMessage);
                        receivedMessage = ss.ReadString();
                    }
                    else
                    {
                        pipeServer.Close();
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
}

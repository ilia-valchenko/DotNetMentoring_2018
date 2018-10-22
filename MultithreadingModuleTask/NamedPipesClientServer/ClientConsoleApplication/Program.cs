using System;
using System.Collections.Generic;

namespace ClientConsoleApplication
{
    class Program
    {
        private const string ServerName = ".";
        private const int NumberOfSendingMessages = 5;

        static void Main()
        {
            var clients = CreateClients();

            for(int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine($"[CLIENT]: Client #{i + 1} started.");

                clients[i].ConnectToServer(ServerName);

                Console.WriteLine($"[CLIENT]: Client #{i + 1} connected to the server.");

                Console.WriteLine($"[CLIENT]: Client #{i + 1} is getting history of messages from the server...");

                var historyOfMessages = clients[i].GetHistoryOfMessagesFromServer();

                for (int j = 0; j < historyOfMessages.Count - 1; j++)
                {
                    Console.WriteLine($"[CLIENT]: #{j + 1} - {historyOfMessages[j]}");
                }

                Console.WriteLine($"[CLIENT]: Client #{i + 1} successfully received the history of messages from the server.");

                Console.WriteLine($"[CLIENT]: Client #{i + 1} is sending messages to the server...");

                clients[i].SendMessagesToServer(NumberOfSendingMessages);

                Console.WriteLine($"[CLIENT]: Client #{i + 1} successfully send {NumberOfSendingMessages} messages to the server.");
            }

            Console.WriteLine("\n\nTap to close the window...");
            Console.ReadKey();
        }

        /// <summary>
        /// Creates initial set of clients.
        /// </summary>
        /// <returns>
        /// Returns the collection of initial clients.
        /// </returns>
        private static List<Client> CreateClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "FirstClient",
                    Messages = new List<string>
                    {
                        "[FirstClient]: Test message #1",
                        "[FirstClient]: Test message #2",
                        "[FirstClient]: Test message #3",
                        "[FirstClient]: Test message #4",
                        "[FirstClient]: Test message #5",
                        "[FirstClient]: Test message #6",
                        "[FirstClient]: Test message #7",
                        "[FirstClient]: Test message #8",
                        "[FirstClient]: Test message #9",
                    }
                },
                new Client
                {
                    ClientName = "SecondClient",
                    Messages = new List<string>
                    {
                        "[SecondClient]: Fake message #1",
                        "[SecondClient]: Fake message #2",
                        "[SecondClient]: Fake message #3",
                        "[SecondClient]: Fake message #4",
                        "[SecondClient]: Fake message #5",
                        "[SecondClient]: Fake message #6",
                        "[SecondClient]: Fake message #7",
                    }
                },
                new Client
                {
                    ClientName = "ThirdClient",
                    Messages = new List<string>
                    {
                        "[ThirdClient]: Message #1",
                        "[ThirdClient]: Message #2",
                        "[ThirdClient]: Message #3",
                        "[ThirdClient]: Message #4",
                        "[ThirdClient]: Message #5",
                        "[ThirdClient]: Message #6",
                        "[ThirdClient]: Message #7",
                        "[ThirdClient]: Message #8",
                        "[ThirdClient]: Message #9",
                        "[ThirdClient]: Message #10"
                    }
                }
            };
        }
    }
}

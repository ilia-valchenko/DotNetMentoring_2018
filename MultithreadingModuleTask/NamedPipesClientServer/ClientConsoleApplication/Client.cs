using System;
using System.Collections.Generic;
using System.IO.Pipes;

namespace ClientConsoleApplication
{
    /// <summary>
    /// The client class.
    /// </summary>
    public class Client
    {
        private const int ConnectionTimeout = 30000;
        private const string StopReadingMessagesCommand = "StopReadingMessagesCommand";
        private const string PipeName = "testpipe";

        private NamedPipeClientStream pipeClient;

        public string ClientName { get; set; }
        public List<string> Messages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
            this.Messages = new List<string>();
        }

        /// <summary>
        /// Connects to a server.
        /// </summary>
        /// <param name="serverName">The name of the server.</param>
        public void ConnectToServer(string serverName)
        {
            this.pipeClient = new NamedPipeClientStream(serverName, PipeName, PipeDirection.InOut, PipeOptions.None);
            pipeClient.Connect(ConnectionTimeout);
        }

        /// <summary>
        /// Sends first N messages to a server. You specify this
        /// number by using count parameter.
        /// </summary>
        /// <param name="count">The number of messages.</param>
        public void SendMessagesToServer(int count)
        {
            if(count > this.Messages.Count)
            {
                throw new ArgumentException("Count is greater than the number of existing messages.");
            }

            var ss = new StreamString(this.pipeClient);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"[CLIENT]: Send '{this.Messages[i]}'");

                ss.WriteString(this.Messages[i]);
            }
        }

        public void DisconnectFromServer()
        {
            pipeClient?.Dispose();
        }

        /// <summary>
        /// Receives messages from a server while none of them are not equal
        /// to stop command.
        /// </summary>
        /// <returns>Returns the list of received messages.</returns>
        public List<string> GetHistoryOfMessagesFromServer()
        {
            var messages = new List<string>();
            var ss = new StreamString(this.pipeClient);

            string receivedMessage = ss.ReadString();

            while (this.pipeClient.IsConnected &&
                   receivedMessage != StopReadingMessagesCommand &&
                   !string.IsNullOrEmpty(receivedMessage))
            {
                messages.Add(receivedMessage);
                receivedMessage = ss.ReadString();
            }

            return messages;
        }
    }
}

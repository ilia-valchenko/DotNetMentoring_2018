using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

namespace ClientConsoleApplication
{
    public class Client
    {
        private const int ConnectionTimeout = 5000;
        private NamedPipeClientStream pipeClient;

        public string ClientName { get; set; }
        public List<string> Messages { get; set; }

        public Client()
        {
            this.Messages = new List<string>();
        }

        public void ConnectToServer(string serverName)
        {
            this.pipeClient = new NamedPipeClientStream(serverName);
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

            using (StreamWriter streamWriter = new StreamWriter(pipeClient))
            {
                for(int i = 0; i < count; i++)
                {
                    streamWriter.WriteLine(this.Messages[i]);
                    streamWriter.Flush();
                }
            }
        }

        public void DisconnectFromServer()
        {
            if(this.pipeClient != null)
            {
                pipeClient.Dispose();
            }
        }
    }
}

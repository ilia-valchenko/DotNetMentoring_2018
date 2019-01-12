using MessageQueueTask.Logger;
using System;
using System.IO;
using System.Messaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace CenterQueueClient
{
    public class CenterQueueClient<T> : ICenterQueueClient
    {
        private readonly string _nameOfCentralQueue;
        MessageQueue _messageQueue;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CenterQueueClient"/> class.
        /// </summary>
        /// <param name="nameOfCentralQueue">The name of the queue.</param>
        /// <param name="logger">The logger.</param>
        public CenterQueueClient(string nameOfCentralQueue, ILogger logger)
        {
            _nameOfCentralQueue = nameOfCentralQueue;
            _logger = logger;
            _messageQueue = new MessageQueue(_nameOfCentralQueue);
            _messageQueue.Formatter = new BinaryMessageFormatter();
        }

        public void Send(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _logger.Info("Sending message to the central queue.");

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, message);
                var binaryMessage = memoryStream.ToArray();
                _messageQueue.Send(binaryMessage);
            }
        }
    }
}

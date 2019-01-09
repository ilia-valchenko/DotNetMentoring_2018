using MessageQueueTask.Logger;
using System;
using System.Messaging;

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
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _logger.Info("Sending message to the central queue.");

            Message recoverableMessage = new Message(message);
            recoverableMessage.Formatter = new BinaryMessageFormatter();

            _messageQueue.Send(recoverableMessage, _nameOfCentralQueue);
        }
    }
}

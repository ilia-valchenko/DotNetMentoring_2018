using CentralManagementServer.Logger;
using System;
using System.Configuration;
using System.Messaging;
using System.IO;
using CentralManagementService.Messages;

namespace CentralManagementService
{
    public class CentralManagementService : ICentralManagementService
    {
        private readonly string _centralQueueName;
        private readonly ILogger _logger;
        private readonly MessageQueue _centralMessageQueue;
        private int _documentNumber = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="CentralManagementService"/> class.
        /// </summary>
        public CentralManagementService(ILogger logger)
        {
            _logger = logger;
            _centralQueueName = ConfigurationManager.AppSettings["centralQueueName"];

            if(MessageQueue.Exists(_centralQueueName))
            {
                _centralMessageQueue = new MessageQueue(_centralQueueName);
            }
            else
            {
                _centralMessageQueue = MessageQueue.Create(_centralQueueName);
            }

            _centralMessageQueue.Formatter = new BinaryMessageFormatter();
        }

        public void StartCentralQueueProcessing()
        {
            _logger.Info("CentralManagementService started processing central queue.");

            Console.WriteLine("Started print messages from central queue:");

            while (true)
            {
                var message = _centralMessageQueue.Receive();

                File.WriteAllBytes(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "ResultPdfDocument" + (++_documentNumber).ToString() + ".pdf"),
                    (byte[])message.Body);

                Console.WriteLine(message.Body.ToString());
            }
        }

        public void StopCentralQueueProcessing()
        {
            throw new NotImplementedException();
        }

        public void SendBroadcastMessage(BaseMessage message)
        {
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _logger.Info("Start sending broadcast message from central management server.");

            Message recoverableMessage = new Message(message);
            recoverableMessage.Formatter = new BinaryMessageFormatter();

            _centralMessageQueue.Send(recoverableMessage);
        }
    }
}

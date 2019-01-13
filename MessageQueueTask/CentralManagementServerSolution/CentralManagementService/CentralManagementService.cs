using CentralManagementServer.Logger;
using System;
using System.Configuration;
using System.Messaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MessageQueueTask.MessagesLibrary.CentralServiceMessages;
using MessageQueueTask.MessagesLibrary.ImageServiceMessages;

namespace CentralManagementService
{
    public class CentralManagementService : ICentralManagementService
    {
        private readonly string _centralQueueName;
        private readonly ILogger _logger;
        private readonly MessageQueue _centralMessageQueue;
        private readonly MessageQueue _multicastMessageQueue;
        private readonly string _resultPdfDocumentName;
        private int _documentNumber;

        /// <summary>
        /// Initialize a new instance of the <see cref="CentralManagementService"/> class.
        /// </summary>
        public CentralManagementService(ILogger logger)
        {
            _logger = logger;
            _documentNumber = 0;
            _centralQueueName = ConfigurationManager.AppSettings["CentralQueueName"];
            _resultPdfDocumentName = ConfigurationManager.AppSettings["ResultPdfDocumentName"];
            string multicastMessageQueueName = ConfigurationManager.AppSettings["MulticastMessageQueueName"];

            if (MessageQueue.Exists(_centralQueueName))
            {
                _centralMessageQueue = new MessageQueue(_centralQueueName);
            }
            else
            {
                _centralMessageQueue = MessageQueue.Create(_centralQueueName);
            }

            _centralMessageQueue.Formatter = new BinaryMessageFormatter();
            _multicastMessageQueue = new MessageQueue(multicastMessageQueueName);
        }

        public void StartCentralQueueProcessing()
        {
            _logger.Info("CentralManagementService started processing central queue.");

            while (true)
            {
                var message = _centralMessageQueue.Receive();

                object deserializedObject = null;

                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    using (MemoryStream memoryStream = new MemoryStream((byte[])message.Body))
                    {
                        deserializedObject = binaryFormatter.Deserialize(memoryStream);
                    }
                }
                catch (Exception exc)
                {
                    _logger.Error("An error has occured during deserialization of binary message from central queue.", exc);
                };

                if(deserializedObject is DocumentWrapperMessage)
                {
                    _logger.Info("Start saving document as a PDF document.");

                    var documentWrapper = (DocumentWrapperMessage)deserializedObject;

                    File.WriteAllBytes(
                        Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            _resultPdfDocumentName + (++_documentNumber).ToString() + ".pdf"),
                        documentWrapper.ITextSharpDocumentBytes);

                    _logger.Info($"The {_resultPdfDocumentName + (++_documentNumber).ToString() + ".pdf"} was saved on the disk.");
                }

                if(deserializedObject is StatusMessage)
                {
                    var statusMessage = (StatusMessage)deserializedObject;

                    _logger.Info($"The status message was received. ServiceName: {statusMessage.ServiceName}; Action: {statusMessage.Action}; FakeSettingsValue: {statusMessage.FakeSettingsValue}.");
                }
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

            Message recoverableMessage = new Message(message)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(BaseMessage) })
            };

            _logger.Info("The message was prepared for sending.");
            _logger.Info(recoverableMessage.Body.ToString());

            try
            {
                _multicastMessageQueue.Send(recoverableMessage);

                _logger.Info("The broadcast message was successfully sent.");
            }
            catch (Exception exc)
            {
                _logger.Error("The broadcast message was not sent.", exc);
            }
        }
    }
}

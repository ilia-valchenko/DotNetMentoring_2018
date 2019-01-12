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
        private int _documentNumber;

        /// <summary>
        /// Initialize a new instance of the <see cref="CentralManagementService"/> class.
        /// </summary>
        public CentralManagementService(ILogger logger)
        {
            _logger = logger;
            _documentNumber = 0;
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

            _multicastMessageQueue = new MessageQueue("formatname:multicast=234.1.1.1:8001")
            {
                //Formatter = new XmlMessageFormatter(new[] {typeof(CentralManagementService.Messages.TestMessage) })
            };
        }

        public void StartCentralQueueProcessing()
        {
            _logger.Info("CentralManagementService started processing central queue.");

            Console.WriteLine("Started print messages from central queue:");

            while (true)
            {
                var message = _centralMessageQueue.Receive();

                bool canCastToStatusMessageType = false;
                bool canCastToDocumentType = false;
                object deserializedObject2 = null;

                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    //var deserializedObject1 = binaryFormatter.Deserialize(message.BodyStream);

                    // TODO: I need to move all messages to separate assembly.



                    using (MemoryStream memoryStream = new MemoryStream((byte[])message.Body))
                    {
                        deserializedObject2 = binaryFormatter.Deserialize(memoryStream);
                        canCastToStatusMessageType = deserializedObject2 is StatusMessage;
                        canCastToDocumentType = deserializedObject2 is iTextSharp.text.Document;
                        int i = 0;
                    }
                }
                catch (Exception exc)
                {
                    _logger.Error("An error has occured during deserialization of binary message from central queue.", exc);
                }

                //File.WriteAllBytes(
                //    Path.Combine(
                //        AppDomain.CurrentDomain.BaseDirectory,
                //        "ResultPdfDocument" + (++_documentNumber).ToString() + ".pdf"),
                //    (byte[])message.Body);

                if(canCastToDocumentType)
                {
                    _logger.Info("Start saving document as a PDF document.");

                    File.WriteAllBytes(
                        Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "ResultPdfDocument" + (++_documentNumber).ToString() + ".pdf"),
                        (byte[])message.Body);
                }

                if(canCastToStatusMessageType)
                {
                    var statusMessage = (StatusMessage)deserializedObject2;

                    _logger.Info($"The status message was received. Action: {statusMessage.Action}; FakeSettingsValue: {statusMessage.FakeSettingsValue}.");

                    Console.WriteLine($"\nAction = {statusMessage.Action}\nFakeSettingsValue = {statusMessage.FakeSettingsValue}");
                }

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

            Message recoverableMessage = new Message(message)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(TestMessage) })
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

using System;

namespace MessageQueueTask.MessagesLibrary.ImageServiceMessages
{
    /// <summary>
    /// This message contains current status of the image service.
    /// </summary>
    [Serializable]
    public class StatusMessage
    {
        public string ServiceName { get; set; }
        public string Action { get; set; }
        public string FakeSettingsValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMessage"/> class.
        /// </summary>
        public StatusMessage()
        {
        }
    }
}
